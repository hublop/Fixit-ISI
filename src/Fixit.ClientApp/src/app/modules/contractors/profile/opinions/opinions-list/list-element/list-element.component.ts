import { Component, OnInit, Input } from '@angular/core';
import { OpinionInProfile } from '../../../../_models/OpinionInProfile';

@Component({
  selector: 'app-list-element',
  templateUrl: './list-element.component.html',
  styleUrls: ['./list-element.component.scss']
})
export class ListElementComponent implements OnInit {

  @Input() opinion: OpinionInProfile;

  constructor() { }

  ngOnInit() {
  }

  getStarStyle(star, rating) {
    const styles = {
      'color': star <= rating ? '#0074D9' : 'lightgray',
      'fill': star <= rating ? '#0074D9' : 'lightgray'
    };

    return styles;
  }
}
