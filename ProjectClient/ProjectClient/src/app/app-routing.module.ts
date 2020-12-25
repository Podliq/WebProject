import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateAdvertisementComponent } from './create-advertisement/create-advertisement.component';
import { EditAdvertisementComponent } from './edit-advertisement/edit-advertisement.component';
import { HomeComponent } from './home/home.component';
import { ListAdvertisementsComponent } from './list-advertisements/list-advertisements.component';
import { LoginComponent } from './login/login.component'
import { MyProfileComponent } from './my-profile/my-profile.component';
import { RegisterComponent } from './register/register.component'
import { AuthGuardService } from './services/auth-guard.service';
import { ShowAdvertisementComponent } from './show-advertisement/show-advertisement.component';

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuardService] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'create', component: CreateAdvertisementComponent, canActivate: [AuthGuardService] },
  { path: 'advertisements', component: ListAdvertisementsComponent, canActivate: [AuthGuardService] },
  { path: 'advertisements/:id', component: ShowAdvertisementComponent, canActivate: [AuthGuardService] },
  { path: 'advertisements/edit/:id', component: EditAdvertisementComponent, canActivate: [AuthGuardService] },
  { path: 'myProfile', component: MyProfileComponent, canActivate: [AuthGuardService] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
