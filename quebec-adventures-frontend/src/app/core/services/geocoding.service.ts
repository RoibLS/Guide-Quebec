import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

export interface LocationResult {
  display_name: string;
  address: {
    city?: string;
    town?: string;
    village?: string;
    state?: string;
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

  searchCity(query: string): Observable<any[]> {
    return this.http.get<any[]>(this.API_URL, {
      params: {
        q: query + ', Quebec, Canada', // On restreint au Québec pour être pertinent
        format: 'json',
        addressdetails: '1',
        limit: '5'
      }
    }).pipe(
      map(results => results.map(r => ({
        name: this.extractCityName(r),
        fullAddress: r.display_name,
        region: r.address.state || r.address.region,
        lat: r.lat,
        lon: r.lon
      })))
    );
  }

  private extractCityName(result: any): string {
    return result.address.city || result.address.town || result.address.village || result.address.municipality || result.display_name.split(',')[0];
  }
}
