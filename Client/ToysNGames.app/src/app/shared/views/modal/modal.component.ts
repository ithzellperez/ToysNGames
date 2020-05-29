import { OnInit, Component, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Product } from '../../product';

@Component({
    selector: 'dialog-overview',
    templateUrl: './modal.component.html',
    styleUrls: ['./modal.component.scss']
  })
  
  export class ShowModal implements OnInit{
  
    form: FormGroup;
    submitInvalid: boolean = true;

    constructor(
      public dialogRef: MatDialogRef<ShowModal>,
      @Inject(MAT_DIALOG_DATA) 
      public data: Product) {}
  
    ngOnInit() {  
      this.form = new FormGroup({
        nameInput: new FormControl(this.data.name),
        companyInput: new FormControl(this.data.company),
        priceInput: new FormControl(this.data.price, [Validators.max(1000), Validators.min(1)]),
        ageRestrictionInput: new FormControl(this.data.ageRestriction, [Validators.max(100), Validators.min(0)]),
        descriptionInput: new FormControl(this.data.description)
      });
    }
  
    onNoClick(): void {
      this.dialogRef.close();
    }
  
    numberOnly(event): boolean {
      const charCode = (event.which) ? event.which : event.keyCode;
      if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        //only numbers accepted
        return false;
      }
      return true;
    }
  
    decimalOnly(event): boolean{
      const charCode = (event.which) ? event.which : event.keyCode;
      if(charCode == 46) //dot (.) keycode
        return true;
      return this.numberOnly(event);      
    }
  
    async onSubmit() {
      if (this.form.valid) {
        try {
          this.data.name = this.form.get('nameInput').value;;
          this.data.company = this.form.get('companyInput').value;
          this.data.price = this.form.get('priceInput').value;
          this.data.description = this.form.get('descriptionInput').value;

          //if no max age provided, send highest valid age
          var age = this.form.get('ageRestrictionInput').value;
          this.data.ageRestriction = age == "" ? "100" : age;

          this.dialogRef.close(this.data);          
        } catch (err) {
          this.submitInvalid = true;
        }
      } else {
        this.submitInvalid = true;
      }
    }
  }