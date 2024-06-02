import { IspModel } from "./ispModel";

// Defins the ISP class
export class ISP {
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

  // Convert ISP to IspModel
  toModel(): IspModel {
    return new IspModel(
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

  // Static method to convert a list of ISP to a list of ISPModel
  static toModelList(isps: ISP[]): IspModel[] {
    return isps.map((isp: ISP) => isp.toModel());
  }
}
