import { CategoryInfoForList } from './../../categories/_models/CategoryInfoForList';
import { Component, OnInit } from '@angular/core';
import { FormControl } from '../../../../../node_modules/@angular/forms';
import { ActivatedRoute } from '../../../../../node_modules/@angular/router';
import { Router } from '@angular/router';
import { ContractorProfile } from '../_models/ContractorProfile';
import { ContractorsService } from '../_services/contractors.service';
import { InfoService } from '../../shared/info/info.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.scss']
})
export class EditProfileComponent implements OnInit {

  selectedTab = new FormControl(0);
  contractorProfile: ContractorProfile;
  categories: CategoryInfoForList[];
  type: string;
  userId: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private contractorsService: ContractorsService,
    private infoService: InfoService
  ) { }

  ngOnInit() {
    this.getRouteData();
  }

  getRouteData() {
    this.route.data.subscribe(data => {
      this.contractorProfile = data['profile'];
      this.categories = data['categories'];
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
    } else if (this.type === 'services') {
      this.selectedTab.setValue(1);
    } else if (this.type === 'change-password') {
      this.selectedTab.setValue(2);
    }
  }

  onDataEdited($event) {
    if ($event) {
      this.getProfile();
    }
  }

  getProfile() {
    this.contractorsService.getContractorProfile(this.userId).subscribe((profile: ContractorProfile) => {
      this.contractorProfile = profile;
    }, error => {
      this.infoService.error('Nie udało się wczytać danych');
    });
  }

  setTabName(index) {
    let editTypeName = 'personal-data';
    if (index === 1) {
      editTypeName = 'services';
    } else if (index === 2) {
      editTypeName = 'change-password';
    }
    this.selectedTab.setValue(index);
    this.router.navigate(['contractors/profile/' + this.userId + '/edit', {editType: editTypeName}]);
  }
}
