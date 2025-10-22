import { Component, inject } from '@angular/core';
import { TitlePage } from '../../shared/components/title-page/title-page';
import {
  FormArray,
  FormBuilder,
  FormControl,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ISystemPayload } from '../../shared/models/system-payload.interface';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { CommonModule } from '@angular/common';
import { ChipModule } from 'primeng/chip';
import { ActivatedRoute, Router } from '@angular/router';
import { FactorySystemsService } from '../../core/services/factory-systems.service';
import { ISystemPostBody } from '../../shared/models/system-post-body.interface';
import { ErrorDialog, IErrorResponse } from "../../shared/components/error-dialog/error-dialog";
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-system-register',
  imports: [
    TitlePage,
    ReactiveFormsModule,
    FormsModule,
    CardModule,
    InputTextModule,
    ButtonModule,
    CommonModule,
    ChipModule,
    ErrorDialog
],
  templateUrl: './system-register.html',
  styleUrl: './system-register.scss',
})
export class SystemRegister {
  private factorySystemsService = inject(FactorySystemsService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private fb = inject(FormBuilder);
  private messageService = inject(MessageService);


  data!: ISystemPayload;
  loading = false
  editMode = false
  newEmail = '';

  showErrorDialog = false;
  errorResponse?: IErrorResponse;

  protected form = this.fb.group({
    applicationName: ['', [Validators.required, Validators.minLength(2)]],
    applicationCode: ['', [Validators.required]],
    costCenter: ['', [Validators.required]],
    emailSupport: this.fb.array<FormControl<string>>([], [Validators.required]),
    status: ['', [Validators.required]],
    database: ['', [Validators.required]],
    installationLocation: ['', [Validators.required]],
  });

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if(id) {
      this.editMode = true;
      this.loadSystemData(id);
    }
  }

  loadSystemData(id: string) {
    this.factorySystemsService.getSystemById(id).subscribe({
      next: (response: ISystemPayload) => {
        this.data = response;
        this.form.patchValue(this.data);
        this.populateEmailSupport(this.data.emailSupport || []);
      }
    })
  }

  populateEmailSupport(emails: string[]) {
    emails.forEach(email => {
      const emailControl = new FormControl(email);
      this.emailSupportArray.push(emailControl);
    });
  }

  onCancel() {
    this.router.navigate(['./']);
  }

  onSubmit() {
    if(this.form.invalid) {
      return;
    }

    if(this.editMode) {
      this.editSystemData();
      return;
    }

    this.postSystemData();
  }

  editSystemData() {
    const id = this.data.id;

    let params: ISystemPayload = {
      id: id,
      applicationName: this.form.value.applicationName!,
      applicationCode: this.form.value.applicationCode!,
      costCenter: this.form.value.costCenter!,
      emailSupport: this.getEmailArray(),
      status: this.form.value.status!,
      database: this.form.value.database!,
      installationLocation: this.form.value.installationLocation!
    };

    this.factorySystemsService.putSystem(id, params).subscribe({
      next: (response) => {
        console.log(response);
        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: 'System updated successfully!',
          life: 3000
        });
        this.router.navigate(['./']);
      },
      error: (error) => {
        console.error(error);
        this.handleError(error);
      }
    })
  }

  postSystemData() {
    let params: ISystemPostBody = {
      applicationName: this.form.value.applicationName!,
      applicationCode: this.form.value.applicationCode!,
      costCenter: this.form.value.costCenter!,
      emailSupport: this.getEmailArray(),
      status: this.form.value.status!,
      database: this.form.value.database!,
      installationLocation: this.form.value.installationLocation!
    };

    this.factorySystemsService.postSystem(params).subscribe({
      next: (response) => {
        console.log(response);
        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: 'System registered successfully!',
          life: 3000
        });
        this.router.navigate(['./']);
      },
      error: (error) => {
        console.error(error);
        this.handleError(error);
      }
    })
  }

  getEmailArray(): string[] {
    const emailControls = this.getEmailControls();
    return emailControls
      .map(control => control.value?.trim())
      .filter(email => email && email.length > 0);
  }

  addEmail() {
    if (this.newEmail) {
      const emailControl = new FormControl(this.newEmail, [Validators.email]);
      this.emailSupportArray.push(emailControl);
      this.newEmail = '';
    }
  }

  removeEmail(index: number) {
    this.emailSupportArray.removeAt(index);
  }

  getEmailControls(): FormControl<string>[] {
    return this.emailSupportArray.controls as FormControl<string>[];
  }

  private handleError(error: any) {
    const errorMessage = error.error?.errorMessage ||
                        error.error?.message ||
                        error.message ||
                        'An unexpected error occurred';

    this.errorResponse = {
      statusCode: error.status || error.error?.statusCode || 500,
      errorMessage: errorMessage,
    };

    this.showErrorDialog = true;
  }

  get emailSupportArray() {
    return this.form.get('emailSupport') as FormArray;
  }
}
