import { Input, Self } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-date-input',
  templateUrl: './date-input.component.html',
  styleUrls: ['./date-input.component.css']
})
// for datePicker reference visit : https://valor-software.com/ngx-bootstrap/#/datepicker
export class DateInputComponent implements ControlValueAccessor {
  @Input() label!: string;
  @Input() maxDate!: Date;
  bsConfig!: Partial<BsDatepickerConfig>; // partial means wvery single property inside BSDatePicker is going to be optional, we dont need to prvide all configuration option

  constructor(@Self() public ngControl: NgControl ) {

    this.ngControl.valueAccessor = this;
    this.bsConfig ={
      containerClass: 'theme-red',
      dateInputFormat: 'DD MMMM YYYY',
      
    }
   }
  writeValue(obj: any): void {
   
  }
  registerOnChange(fn: any): void {
    
  }
  registerOnTouched(fn: any): void {
   
  }

  ngOnInit(): void {
  }

}
