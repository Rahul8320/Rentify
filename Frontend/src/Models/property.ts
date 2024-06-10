import { PropertyModel } from "./propertyModel";

// Defins the Property class
export class Property {
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

  // Convert Property to PropertyModel
  toModel(): PropertyModel {
    return new PropertyModel(
      this.id,
      this.place,
      this.noOfBedroom,
      this.noOfBathroom,
      this.sizeinSqft,
      this.nearbySchool,
      this.nearbyCollege,
      this.nearbyHospital,
      this.description,
      this.price,
      this.lastUpdated
    );
  }

  // Static method to convert a list of Property to a list of PropertyModel
  static toModelList(properties: Property[]): PropertyModel[] {
    return properties.map((property: Property) => property.toModel());
  }
}
