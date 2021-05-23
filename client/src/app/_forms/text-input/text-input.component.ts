import { Self } from '@angular/core';
import { Component, Input} from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

// We want to aceess all fromControl here that we are accessing in resgister component
// for that we will implement interface ControlValueAccessor
// we will access all native formControl like formControlName 
// For @Self explanation visit : https://medium.com/frontend-coach/self-or-optional-host-the-visual-guide-to-angular-di-decorators-73fbbb5c8658

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.css']
})
export class TextInputComponent implements ControlValueAccessor {
  @Input() label!: string;
  @Input() type = 'text';

  constructor(@Self() public ngControl: NgControl ) { 
    this.ngControl.valueAccessor = this;
  } // it will inject what we are doing loaclly inside this component
  
  writeValue(obj: any): void {
    
  }
  registerOnChange(fn: any): void {
    
  }
  registerOnTouched(fn: any): void {
    
  }

  ngOnInit(): void {
  }

}
