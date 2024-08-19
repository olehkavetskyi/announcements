import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, withComponentInputBinding } from '@angular/router';

import { routes } from './app.routes';
import { provideToastr } from 'ngx-toastr';
import { provideClientHydration } from '@angular/platform-browser';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideAnimations } from '@angular/platform-browser/animations';
import { NgxSpinnerModule } from 'ngx-spinner';
import { loadingInterceptor } from './shared/interceptors/loading.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(routes, withComponentInputBinding()), 
    provideClientHydration(),
    provideAnimations(), 
    provideToastr(),
    provideHttpClient(
      withFetch(),
      withInterceptors([loadingInterceptor]),
      ), 
    provideAnimationsAsync(),
    importProvidersFrom(NgxSpinnerModule.forRoot()),
    provideAnimations(),
    provideToastr({
      timeOut: 2000,
      positionClass: 'toast-top-right',
      preventDuplicates: false,
    }),
  ]
};
