import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Component, Inject } from '@angular/core';

@Component({
    selector: 'dialog-overview-deleteitem',
    templateUrl: './confirmation.component.html',
    styleUrls: ['./confirmation.component.scss']
  })
  
  export class DeleteConfirmationModal {
    constructor(
      public dialogRef: MatDialogRef<DeleteConfirmationModal>,
      @Inject(MAT_DIALOG_DATA) 
      public data: {text: ''}) {}
  
    onNoClick(): void {
      this.dialogRef.close();
    }
  }