import { AfterViewInit, Component, OnInit, ViewChild, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { ProductService } from '../shared/product-service';
import { Product } from '../shared/product';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ShowModal } from '../shared/views/modal/modal.component';
import { SnackBarComponent } from '../shared/views/snack-bar/snack-bar.component';
import { DeleteConfirmationModal } from '../shared/views/confirmation/confirmation.component';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements AfterViewInit, OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatTable) table: MatTable<Product>;

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['id', 'name', 'description', 'ageRestriction', 'price', 'company', 'actions'];
  isLoadingResults = true;
  isRateLimitReached = false;
  durationInSeconds = 5;
  productData: { 
    id: 0, 
    name:'', 
    description: '', 
    ageRestriction: 0, 
    price: 0, 
    company: '',
    title: '' 
  };

  public products: Product[];

  constructor(private data: ProductService, private router: Router, private dialog: MatDialog, private _snackBar: MatSnackBar,) { }

  ngOnInit(): void {
    
    this.loadTable();    
  }

  ngAfterViewInit() {
    this.table.dataSource = this.products;

  }

  loadTable():void{
    this.data.getAllProducts()
        .subscribe(success => {
            if (success) {
                this.products = this.data.products;
                this.isLoadingResults = false;
            }
            else{
              this.isRateLimitReached = true;
            }
        });
  }

  addNew(): void {
    let self = this;
    const dialogRef = self.dialog.open(ShowModal, {
      width: '450px',
      data: {
        id: Number(0),
        name: '',
        description: '',
        ageRestriction: '100', //max valid age
        price: '1', //min valid price
        company: '',
        title: 'Add new product'
      }
    });
  
    dialogRef.afterClosed().subscribe((result:Product) => {
      if(!!result){
        self.data.addNewItem(result).subscribe(()=>{
          self.openSnackBar('Product saved');
          self.loadTable();
        });
      }
    });
  }

  updateItem(item): void {
    let self = this;
    const dialogRef = self.dialog.open(ShowModal, {
      width: '450px',
      data: {
        id: item.id,
        name: item.name,
        description: item.description,
        ageRestriction: item.ageRestriction,
        price: item.price,
        company: item.company,
        title: 'Update product'
      },
    });
  
    dialogRef.afterClosed().subscribe(result => {
      if(!!result){
        self.data.updateItem(result).subscribe(()=>{
          self.openSnackBar('Product updated');
          self.loadTable();
        });
      }
    });
  }

  deleteItem(item){
    let self = this;
    const dialogRef = self.dialog.open(DeleteConfirmationModal, {
      width: '315px',
      data: item
    });

    dialogRef.afterClosed().subscribe(result => {
      if(!!result){
        self.data.deleteItem(result).subscribe(()=>{          
          self.openSnackBar('Product deleted');
          self.loadTable();
        });
      }
    });
  }

  openSnackBar(item) {
    let self = this;
    self._snackBar.openFromComponent(SnackBarComponent, {
      duration: self.durationInSeconds * 1000,
      data: { text: item }
    });
  }
}