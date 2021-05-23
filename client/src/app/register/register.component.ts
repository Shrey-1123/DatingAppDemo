import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
 // @Input() usersFromHomeComponent : any;
  @Output() cancelRegister = new EventEmitter();
  //model:any = {};
  registerForm!: FormGroup;
  maxDate!:Date;
  validationErrors:string[]=[];

  constructor(private accountService: AccountService,private toastr:ToastrService,
     private fb: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.intializeForm();
    this.maxDate = new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear()-18);
  }
   
  // intializeForm(){
  //   this.registerForm = new FormControl({

  //     username: new FormControl('', Validators.required),
  //     password: new FormControl('',[Validators.required, Validators.minLength(4), Validators.maxLength(8)]),
  //     confirmPassword: new FormControl('', [Validators.required, this.matchValues('password')]) //  we will make custom validator for confirm password
  //   })
  //   this.registerForm.controls.password.valueChanges.subscribe(()=>{
  //     this.registerForm.controls.confirmPassword.updateValueAndValidity();
  //   })
  // }

  // mosre optimised code
  intializeForm(){
    this.registerForm = this.fb.group({

      gender: ['male'],
      username: ['', Validators.required],
      knownAs: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['',[Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]] //  we will make custom validator for confirm password
    })
    this.registerForm.controls.password.valueChanges.subscribe(()=>{
      this.registerForm.controls.confirmPassword.updateValueAndValidity();
    })
  }

  matchValues(matchTo: string): ValidatorFn{
    return (control: AbstractControl | any)=>{
       return control?.value === control?.parent?.controls[matchTo].value 
        ? null : {isMatching:true}
      // control?value mera confirm password vala section h and hmlog jo bhi matchTo mien pass krenge vo 
      // hmara control?parent vala hoga sa hmlog matchTo mien password pass krenge
    }
  }
   register(){
     //console.log(this.registerForm.value);
     
      this.accountService.register(this.registerForm.value).subscribe(response=>{
        //console.log(response);
        this.router.navigateByUrl('/members');
        //this.cancel();
      },error=>{
        console.log(error);
        //this.toastr.error(error.error); 
        this.validationErrors = error;
      });
   }

  cancel(){
    this.cancelRegister.emit(false); // we want cancel button to emit false which means . on click event will be emiited using EventEmitter and false will be returnered from cancel()

  }
}
