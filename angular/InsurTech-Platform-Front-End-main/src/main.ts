import { provideCharts, withDefaultRegisterables } from 'ng2-charts';
import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { appConfig } from './app/app.config';

// Extend appConfig with the ng2-charts providers
const extendedAppConfig = {
  ...appConfig,
  providers: [
    ...appConfig.providers,
    provideCharts(withDefaultRegisterables())
  ],
};

bootstrapApplication(AppComponent, extendedAppConfig)
  .catch((err) => console.error(err));
