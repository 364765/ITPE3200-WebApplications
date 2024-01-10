  import {Component, OnInit} from '@angular/core';
  import {RentalService} from "../Rentals/rentals.service";
  import {IRental} from "../Rentals/rental";
  import {HttpClient} from "@angular/common/http";
  import { Router } from "@angular/router";
  import { trigger, transition, style, animate } from '@angular/animations';

  @Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
     animations: [
      trigger('colorHeart', [
        transition('white <=> accentS', [
          style({
            transform: `scale(1.5)`,
            opacity: 0
          }),
          animate('.2s 0s ease-out'),
        ])
      ])
    ]
  })
  export class HomeComponent implements OnInit{
    viewTitle : string = 'Table';
    displayImage: boolean = true;
    rentals: IRental[] = [];
    //listFilter: string = '';

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

    /* Gets all rentals */
    getRentals(): void {
      this._rentalService.getRentals()
        /* Gets all the rentals through rentalService */
        .subscribe(data => {
          this.rentals = data.map(rental => ({ ...rental, favorited: false }));
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

    /* Navigates to the details view of the specified rental */
    detailsRental(rentalId: number) {
      console.log("this is the id for the rental: ", rentalId);
      this._router.navigate(['/Rental/details', rentalId]);
    }

    /* Navigates to the details page of the specified rental */
    navigateToDetails(rentalId: number) {
      console.log("this is the id for the rental: ", rentalId);
      this._router.navigate(["/details", rentalId])
    }

    /*Navigates to the favorites page */
    navigateToFavorites() {
      this._router.navigate(["/favorites"]);
    }

    ngOnInit(): void {
      console.log("RentalsComponent created");
      /* Gets all rentals */
      this.getRentals();
    }

    toggleButton(rental: IRental) {
      /* Checks to see if rental has been favorited before (favorited = true) */
      if (!rental.Favorited) {
        /* If it hasn't been favorited, it calls favoriteRental */
        this.favoriteRental(rental);
      } else {
        /* If it has been favorited, it calls removeRentalFromFavorite */
        this.removeRentalFromFavorite(rental);
      }
    }

    favoriteRental(rental: IRental) {
      /* Makes rental.Favorited true*/
      rental.Favorited = true;
      /* Logs what value the favortied variable has */
      console.log("Rental " + rental.RentalId + " favorited : " + rental.Favorited);
    }

    removeRentalFromFavorite(rental: IRental) {
      /* Makes rental.Favorited false */
      rental.Favorited = false;
      /* Logs what value the favortied variable has */
      console.log("Rental " + rental.RentalId + " favorited : " + rental.Favorited);
     }
  }



