import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginServiceService } from 'src/app/services/login-service.service';
import { LoginForm } from 'src/app/shared/domain/login/login-form';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  loginInfo: LoginForm;
  loginForm: FormGroup;
  userSession:any;
  constructor(private loginservice:LoginServiceService, private fb:FormBuilder, private router: Router){
    this.loginInfo = {
      username: '',
      password: ''
    };
    this.loginForm = fb.group({
      username: ['',],
      password: ['', [Validators.required]],
    });
  }
  ngOnInit(){
  }

  submitForm() {
    if(!this.loginForm.valid){
      Object.keys(this.loginForm.controls).forEach(field => {
        const control = this.loginForm.get(field);
        control?.markAsDirty();
      });
     return;
    }
   this.loginservice.login(this.loginInfo).subscribe((res:any)=>{
    this.userSession = res
    if(this.userSession.result.result){
      this.router.navigate(['/home']);
    }
    console.log(this.userSession)
   })
  }

}
