export interface ISystemPayload {
  id: string;
  applicationName: string;
  applicationCode?: string;
  costCenter: string;
  emailSupport?: string[];
  status?: string;
  database?: string;
  installationLocation?: string;
}
