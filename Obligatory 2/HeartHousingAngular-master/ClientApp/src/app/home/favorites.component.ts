import { Component, OnInit} from '@angular/core';
import { RentalService } from "../Rentals/rentals.service";
import { IRental } from "../Rentals/rental";

/*

  Because of inhability to fix our problem with the favorited variable, and it keeping the value given on
  the home page. 
  Because of these problems we have not been able to test out this code 

*/



@Component({
  selector: 'app-home',
  templateUrl: './favorites.component.html',
  styleUrls: ['./home.component.css']
})

export class FavoritesComponent implements OnInit{


  ngOnInit() {
    this.getRentals();
  }

  rentals: IRental[] = [];
  favoriteRentals: IRental[] = [];

  constructor(private _rentalService: RentalService) { }

  getRentals(): void {
    console.log("Inne i getRentals favorites");
    this._rentalService.getRentals().subscribe(data => {
      console.log('API Response:', data);
      this.rentals = data;
      this.favoriteRentals = this.getFavorites();
      console.log('Favorite Rentals:', this.favoriteRentals);
    });
  }

  getFavorites(): IRental[] {
    console.log('All Rentals:', this.rentals);
    const favorites = this.rentals.filter(rental => rental.Favorited === true);
    console.log('Favorites:', favorites);
    return favorites;
  }
}
