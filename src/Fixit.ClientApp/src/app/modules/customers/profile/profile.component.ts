import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '../../../../../node_modules/@angular/router';
import { CustomerService } from '../_services/customer.service';
import { CustomerPersonalData } from '../_models/CustomerPersonalData';
import { FormControl } from '@angular/forms';
import { InfoService } from '../../shared/info/info.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  selectedTab = new FormControl(0);
  customerProfile: CustomerPersonalData;
  type: string;
  userId: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private customerService: CustomerService,
    private infoService: InfoService
  ) { }

  ngOnInit() {
    this.getRouteData();
  }

  getRouteData() {
    this.route.data.subscribe(data => {
      this.customerProfile = data['profile'];
    }, error => {
      this.infoService.error('Nie udało się wczytać danych.');
    });
    this.route.params.subscribe(params => {
      this.type = params['editType'];
      this.userId = +params['id'];
    });
    this.setTabNumber();
  }

  setTabNumber() {
    if (this.type === 'personal-data') {
      this.selectedTab.setValue(0);
    } else if (this.type === 'change-password') {
      this.selectedTab.setValue(1);
    }
  }

  onDataEdited($event) {
    if ($event) {
      this.getProfile();
    }
  }

  getProfile() {
    this.customerService.getPersonalData(this.userId).subscribe((profile: CustomerPersonalData) => {
      this.customerProfile = profile;
    }, error => {
      this.infoService.error('Nie udało się wczytać danych.');
    });
  }

  setTabName(index) {
    let editTypeName = 'personal-data';
    if (index === 1) {
      editTypeName = 'change-password';
    }
    this.selectedTab.setValue(index);
    this.router.navigate(['customers/profile/' + this.userId + '/edit', {editType: editTypeName}]);
  }

}
