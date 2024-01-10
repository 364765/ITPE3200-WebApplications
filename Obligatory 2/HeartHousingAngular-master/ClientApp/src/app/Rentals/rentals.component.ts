import { Component, OnInit } from "@angular/core";
import { IRental } from "./rental";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { RentalService } from './rentals.service';

@Component({
  selector : "app-rentals",
  templateUrl: "./rentals.component.html",
  styleUrls: ["./rentals.component.css"]
})

export class RentalsComponent implements OnInit{
  viewTitle : string = 'Table';
  displayImage: boolean = true;
  rentals: IRental[] = [];

  constructor(private _http: HttpClient, private _router: Router, private _rentalService: RentalService) { }

  private _listFilter: string = '';
  get listFilter(): string {
    return this._listFilter;
  }
  set listFilter(value: string) {
    /* Gets the value from the input and makes that the filter */
    this._listFilter = value;
    /* Logs what is in the input, and therefore used as filter */
    console.log("In setter: ", value);
    /* Makes the filtered list, list is the rentals with the value in their name */
    this.filteredRentals = this.performFilter(value);
  }

  deleteRental(rental: IRental): void {
    /* Gives the user a pop-up to ask if they are sure they want to delete the rental */
    const confirmDeleteRental = confirm(`Are you sure you want to delete the rental "${rental.Name}"?`);
    /* If confirmDeleteRental is true */
    if (confirmDeleteRental) {
      /* Deletes the rental through rentalservice */
      this._rentalService.deleteRental(rental.RentalId)
        .subscribe(
          (response) => {
            /* If the deletion was successfull */
            if (response.success) {
              /* Logs the sucess message */
              console.log(response.message);
              /* Removes the rental from filteredRentals */
              this.filteredRentals = this.filteredRentals.filter(i => i !== rental);
            }
          },
          (error) => {
            /* Logs an error message if it does not manage to delete the rental */
            console.error("Error deleting rental: ", error);
          });
    }
  }

  /* Gets all rentals */
  getRentals(): void {
    this._rentalService.getRentals()
    /* Gets all the rentals through rentalService */
      .subscribe(data => {
        /* Logs the data for all the data found by rentalServices's getRentals() */
        console.log('All', JSON.stringify(data));
        /* Makes the gatherered data our rental list */
        this.rentals = data;
        /* Also make the filtered list into the same list */ 
        this.filteredRentals = this.rentals;
      }
      );
  }

  filteredRentals: IRental[] = this.rentals;

  performFilter(filterBy: string): IRental[] {
    /* Makes all the letters in the filter string lower case */
    filterBy = filterBy.toLocaleLowerCase();
    /* Returns the list items that includes the filter string */
    return this.rentals.filter((rentals: IRental) =>
      rentals.Name.toLocaleLowerCase().includes(filterBy));
  }

  /* Toggles image as shown and not shown */
  toggleImage(): void{
    this.displayImage = !this.displayImage
  }

  /* Navigates to rentalform */
  navigateToRentalform() {
    this._router.navigate(["/rentalform"])
  }

  ngOnInit(): void {
    /* Gets all rentals */
    this.getRentals();
    console.log("RentalsComponent created");
  }
}


