import { Component, ViewChild } from '@angular/core';
import { MapInfoWindow, MapMarker, GoogleMap } from '@angular/google-maps';
import {NgForOf} from "@angular/common";

@Component({
  selector: 'app-explore-map',
  templateUrl: './explore-map.component.html',
  standalone: true,
  imports: [
    GoogleMap,
    MapInfoWindow,
    MapMarker,
    NgForOf
  ],
  styleUrls: ['./explore-map.component.css']
})
export class ExploreMapComponent {
  @ViewChild(GoogleMap, { static: false }) map!: GoogleMap;
  @ViewChild(MapInfoWindow, { static: false }) infoWindow!: MapInfoWindow;

  center: google.maps.LatLngLiteral = {lat: 40, lng: -20};
  zoom = 4;
  markerOptions: google.maps.MarkerOptions = {draggable: false};
  markerPositions: google.maps.LatLngLiteral[] = [];

  addMarker(event: google.maps.MapMouseEvent) {
    this.markerPositions.push(event.latLng!.toJSON());
  }

  openInfoWindow(marker: MapMarker) {
    this.infoWindow.open(marker);
  }
}
