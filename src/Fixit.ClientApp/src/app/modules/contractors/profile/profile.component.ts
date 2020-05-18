import { AuthService } from './../../auth/_services/auth.service';
import { ContractorProfile } from './../_models/ContractorProfile';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '../../../../../node_modules/@angular/router';
import { Router } from '@angular/router';
import { InfoService } from '../../shared/info/info.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  contractorProfile: ContractorProfile;

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private router: Router,
    private infoService: InfoService
  ) { }

  ngOnInit() {
    this.getProfileDataFromResolver();
  }

  getProfileDataFromResolver(): void {
    this.route.data.subscribe(data => {
      this.contractorProfile = data['profile'];
    }, error => {
      this.infoService.error('Nie udało się wczytać danych');
    });
  }

  canEditProfile(): boolean {
    const loggedInUser = this.authService.getloggedInUser();
    if (!loggedInUser) {
      return false;
    }

    return loggedInUser.id === this.contractorProfile.id;
  }

  navigateToEdit() {
    this.router.navigate(['contractors/profile/' + this.contractorProfile.id + '/edit', {editType: 'personal-data'}]);
  }

}
