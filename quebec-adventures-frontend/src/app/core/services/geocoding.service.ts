import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

export interface GeocodingResult {
  name: string;
  fullAddress: string;
  region: string | undefined;
  lat: string;
  lon: string;
}

interface LocationResult {
  display_name: string;
  address: {
    city?: string;
    town?: string;
    village?: string;
    municipality?: string;
    state?: string;
    region?: string;
  };
  lat: string;
  lon: string;
}

@Injectable({
  providedIn: 'root'
})
export class GeocodingService {
  // API OpenStreetMap gratuite (respecter les limites d'usage : 1 req/sec max)
  private readonly API_URL = 'https://nominatim.openstreetmap.org/search';

  constructor(private http: HttpClient) {}

  searchCity(query: string): Observable<GeocodingResult[]> {
    return this.http.get<LocationResult[]>(this.API_URL, {
      params: {
        q: query + ', Quebec, Canada',
        format: 'json',
        addressdetails: '1',
        limit: '5'
      }
    }).pipe(
      // On transforme LocationResult → GeocodingResult
      map(results => results.map(r => ({
        name: this.extractCityName(r),
        fullAddress: r.display_name,
        region: r.address.state ?? r.address.region,
        lat: r.lat,
        lon: r.lon
      })))
    );
  }

  private extractCityName(result: LocationResult): string {
    return (
      result.address.city ??
      result.address.town ??
      result.address.village ??
      result.address.municipality ??
      result.display_name.split(',')[0]
    );
  }
}
