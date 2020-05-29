import { Inject, Component } from '@angular/core';
import { MAT_SNACK_BAR_DATA } from '@angular/material/snack-bar';

@Component({
    selector: 'snack-bar-component',
    templateUrl: './snack-bar.component.html',
    styles: [`
      .snack-bar {
        color: white;
      }
    `],
  })
  
  export class SnackBarComponent {
    constructor( @Inject(MAT_SNACK_BAR_DATA) public data: {text: ''}){}
  }
  