import { Component } from "@angular/core";
import { FormGroup, FormControl, Validators, FormBuilder, ReactiveFormsModule } from "@angular/forms";
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from "@angular/common/http";
import { RentalService } from './rentals.service'

@Component({
  selector: "app-rentals-rentalform",
  templateUrl: "./rentalform.component.html",
  styleUrls: ["./rentals.component.css"]
})
export class RentalformComponent {
  rentalForm: FormGroup;
  isEditMode: boolean = false;
  rentalId: number = -1;

  constructor(private _formbuilder: FormBuilder, private _router: Router, private _http: HttpClient,
    private _rentalService: RentalService, private _route: ActivatedRoute) {
    this.rentalForm = _formbuilder.group({
      Name: ['', Validators.required],
      Address: ['', Validators.required],
      PricePrNight: [0, Validators.required],
      RentalType: ['', Validators.required],
      BedNr: [0, Validators.required],
      BathNr: [0, Validators.required],
      Area: [0, Validators.required],
      Description: [''],
      ImageUrl: [''],
      ImageUrl2: [''],
      ImageUrl3: ['']
    });
  }

  onSubmit() {
    console.log("RentalCreate form submittet:");
    console.log(this.rentalForm);
    /* Takes the values from the form and "makes" a new rental item */
    const newRental = this.rentalForm.value;
    if (this.isEditMode) { /* Checks which mode the form is in */
    /* If it's in edit mode, we update the existing rental item */
      this._rentalService.updateRental(this.rentalId, newRental)
        .subscribe(response => {
          /* If the update is a success */
          if (response.success) {
            console.log(response.message);
            /* Navigates back to the rental page when the update is successful */
            this._router.navigate(['/Rentals']);
          } else {
            /* Logs an error message if the update was not successfull */
            console.log('Rental update failed');
          }
        });
    /* If not in editMode */
    } else {
      /* Creates a new rental */
      this._rentalService.createRental(newRental)
        .subscribe(response => {
          /* Checks whether the creation was a success */
          if (response.success) {
            console.log(response.message);
            /* Navigates to the rental page if the creation was a success */
            this._router.navigate(['/Rentals']);
          }
          else {
            /* Logs an error message if the creation was not successfull */
            console.log('Rental creation failed');
          }
        });
    }
  }

  backToRentals() {
    this._router.navigate(["/rentals"])
  }

  ngOnInit(): void {
    this._route.params.subscribe(params => {
      /* If the mode is create */
      if (params['mode'] === 'create') {
        /* Sets isEdit to false */
        this.isEditMode = false;
      /* If the mode is edit */
      } else if (params['mode'] === 'edit') {
        /* Sets isEdit to true */
        this.isEditMode = true;
        /* Converts the value into a number */
        this.rentalId = +params['id'];
        /* Sends the rentalId into loadRentalForEdit */
        this.loadRentalForEdit(this.rentalId);
      }
    });
  }

  loadRentalForEdit(rentalId: number) {
    /* Gets the corresponding rental to the rentalI*/
    this._rentalService.getRentalById(rentalId)
      .subscribe(
        (rental: any) => {
          console.log('retrieved rental: ', rental);
          /* Retrieves the value and puts said value in the edit form */
          this.rentalForm.patchValue({
            Name: rental.Name,
            Address: rental.Address,
            PricePrNight: rental.PricePrNight,
            RentalType: rental.RentalType,
            BedNr: rental.BedNr,
            BathNr: rental.BathNr,
            Area: rental.Area,
            Description: rental.Description,
            ImageUrl: rental.ImageUrl,
            ImageUrl2: rental.ImageUrl2,
            ImageUrl3: rental.ImageUrl3
          });
        },
        (error: any) => {
          /* Logs an error message if it can't load the data for the editing of the rental */
          console.error('Error loading rental for edit: ', error);
        }
      );
  }
}
