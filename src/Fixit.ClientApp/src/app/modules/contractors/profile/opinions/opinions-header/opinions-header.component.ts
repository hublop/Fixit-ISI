import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-opinions-header',
  templateUrl: './opinions-header.component.html',
  styleUrls: ['./opinions-header.component.scss']
})
export class OpinionsHeaderComponent implements OnInit {

  @Input() avgQuality: number;
  @Input() avgPunctuality: number;
  @Input() avgInvolvement: number;
  @Input() avgRating: number;
  @Input() opinionsCount: number;

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
