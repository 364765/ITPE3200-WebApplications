import { Component, OnInit } from "@angular/core";
import { IRental } from "./rental";
import { HttpClient } from "@angular/common/http";
import { ActivatedRoute ,Router } from "@angular/router";
import { RentalService } from './rentals.service';
import { trigger, transition, style, animate } from '@angular/animations';

@Component({
  selector : "app-details",
  templateUrl: "./details.component.html",
  styleUrls: ["./details.component.css"],
  animations: [
    trigger('slideAnimation', [
      transition('void => *', [
        style({ opacity: 0 }),
        animate('500ms ease-in-out', style({opacity: 1}))
      ]),
      transition('* => void', [
        animate('500ms eas-in-out', style({opacity: 0}))
      ])
    ])
  ]
})

export class DetailsComponent implements OnInit{
  viewTitle : string = 'Details';
  rental: any;
  rentalId: number = -1;
  //listFilter: string = '';
  currentindex: number = 0;
  pricePerNight: number = 0; // Added to calculate TotalPrice
  constructor(private _http: HttpClient, private _router: Router,
    private _rentalService: RentalService, private _route: ActivatedRoute) { }


  loadRentailDetails(rentalId: number): void {
    this._rentalService.getRentalById(rentalId).subscribe(
      (data) => {
        this.rental = data; /* The data found by getRentalById gets put into our rental variable */
        console.log("Rental data: ", data);
      },
      (error) => {
        console.error("Couldn't fetch rentail details: ", error);

      }
    );
  }

  backToRentals() {
    this._router.navigate(["/home"])
  }

  ngOnInit(): void {
    console.log("Details component ngOniti called");
    this._route.params.subscribe(params => {
      console.log("Route params: ", params);
      this.rentalId = +params['id']; /* Converts the value into a number */
      console.log("Rentalid: ", this.rentalId);
        this.loadRentailDetails(this.rentalId); /* sends the rentalId into the loadRentailDetails */
    });
  }

  navigateToCreateOrder(rentalId: number) {
    console.log("this is the id for the rental: ", rentalId);
    this._router.navigate(["/orderform/create", rentalId]) /* Goes to the create form for order, with the corrisponding rentalId*/
  }

  /* Go to the next image */
  nextImage() {
    this.currentindex = (this.currentindex + 1) % 3; /* Goes to the next image. If the image it is on, is the last image, it will loop to the first image*/
  }

  /* Go to the previous image */
  prevImage() {
    this.currentindex = (this.currentindex - 1 + 3) % 3; /* Goes to the previous image. If the image it is on, is the first image, it will loop back to the last image*/
  }
}


