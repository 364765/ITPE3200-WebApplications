import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from '@angular/router';
import { OrderService } from "./orders.service";
import { RentalService } from "../Rentals/rentals.service";
import { IRental } from "../Rentals/rental";


@Component({
  selector: "app-orders-orderform",
  templateUrl: "./orderform.component.html",
  styleUrls: ["./orders.component.css"]
})

export class OrderformComponent {
  orderForm: FormGroup;
  isEditMode: boolean = false;
  orderId: number = -1;
  rentalId: number = -1;
  PricePrNight: number = 0; //Lagt til

  constructor(
    private _formbuilder: FormBuilder,
    private _router: Router,
    private _http: HttpClient,
    private _orderService: OrderService,
    private _rentalService: RentalService,
    private _route: ActivatedRoute) {
    this.orderForm = _formbuilder.group({
      RentalId: [0, Validators.required], // Lagt til
      NightsNr: [0, Validators.required],
      TotalPrice: [null, Validators.required] // Endret til null
    });
  }

  onSubmit() {
    console.log("OrderCreate form submittet:");
    console.log(this.orderForm);
    const newOrder = this.orderForm.value;
    newOrder.rentalId = this.rentalId

    if (this.isEditMode) {
      this._orderService.updateOrder(this.orderId, newOrder)
        .subscribe(response => {
          if (response.success) {
            console.log(response.message);
            this._router.navigate(['/Orders']);
          }
          else {
            console.log('Order update failed');
          }
        });
    }
    else {
      this._orderService.createOrder(newOrder)
        .subscribe(response => {
          if (response.success) {
            console.log(response.message);
            this._router.navigate(['/Orders']);
          }
          else {
            console.log('Order creation failed');
          }
        });
    }
  }

  backToOrders() {
    this._router.navigate(['/orders']);
  }

  ngOnInit(): void {

    this._route.params.subscribe(params => {
      if (params['mode'] === 'create') {
        this.rentalId = +params['id'];
        this.isEditMode = false;
        this.loadRentalDetails(this.rentalId); // Call to fetch details
      } else if (params['mode'] === 'edit') {
        this.isEditMode = true;
        this.orderId = +params['id'];
        this.loadOrderForEdit(this.orderId);
      }
    });
    // Gets the rentalId to displat when creating an order
    this.orderForm.get('RentalId')?.setValue(this.rentalId);
  }



  loadOrderForEdit(orderId: number) {
    this._orderService.getOrderById(orderId)
      .subscribe(
        (order: any) => {
          console.log('retrieved order: ', order);
          this.orderForm.patchValue({
            NightsNr: order.NightsNr,
            TotalPrice: order.TotalPrice
          });

          this.rentalId = order.rentalId;
          // Call loadRentalDetails after updating the rentalId
          this.loadRentalDetails(this.rentalId);
        },
        (error: any) => {
          console.error('Error loading order for edit: ', error);
        }
      );
  }


  // Fetching rental details (pricePrNight)
  loadRentalDetails(rentalId: number): void {
    this._rentalService.getRentalById(rentalId).subscribe(
      (rental: IRental) => {
        if (rental) {
          this.PricePrNight = rental.PricePrNight;
          console.log('Retrieved rental details: ', rental);

          // Update total price after fetching pricePrNight
          this.updateTotalPrice(this.orderForm.get('NightsNr')?.value, this.PricePrNight);

        } else {
          console.error('Invalid pricePrNight:', this.PricePrNight);
        }
      },
      (error: any) => console.error('Error fetching rental details: ', error),
      () => {
        // After fetching rental details, call rengUt with NightsNr
        this.rengUt(this.orderForm.get('NightsNr')?.value);

      }
    );
  }

  rengUt(nightNr: number) {
     this._rentalService.getRentalById(this.rentalId).subscribe(
      (rental: IRental) => {
        if (!isNaN(rental.PricePrNight)) {
          this.PricePrNight = rental.PricePrNight;
          const TotalPrice = nightNr * this.PricePrNight;
          console.log("Total prisen er: " + TotalPrice);

          // Update total price after fetching PricePrNight
          this.updateTotalPrice(nightNr, this.PricePrNight);
          console.log("New PricePrNight: " + this.PricePrNight);
        } else {
          console.error('Invalid PricePrNight:', rental.PricePrNight);
        }
      },
      (error: any) => console.error('Error fetching PricePrNight: ', error)
    );
  }

  private updateTotalPrice(nightNr: number, pricePrNight: number) {
    const TotalPrice = nightNr * pricePrNight;
    if (!isNaN(TotalPrice)) {
      console.log("Total prisen er: " + TotalPrice);
      this.orderForm.get('TotalPrice')!.setValue(TotalPrice);
      console.log('New TOTAL PRICE: ' + TotalPrice);
    } else {
      console.error('Invalid calculation result');
    }
  }

}
