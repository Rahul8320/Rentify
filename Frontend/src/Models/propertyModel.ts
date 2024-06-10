export class PropertyModel {
  constructor(
    public id: string,
    public place: string,
    public noOfBedroom: number,
    public noOfBathroom: number,
    public sizeinSqft: number,
    public nearbySchool: boolean,
    public nearbyCollege: boolean,
    public nearbyHospital: boolean,
    public description: string,
    public price: number,
    public lastUpdated: string
  ) {}
}
