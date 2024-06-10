import axios from "axios";
import appConfig from "@/config/appConfig";
import { Property } from "@/Models/property";

class PropertyService {
  apiUrl = appConfig.BaseUrl + "/property";

  // get all property details
  async getAllProperties(): Promise<Property[]> {
    try {
      const result = await axios.get(this.apiUrl);
      return result.data.map(
        (property: Property) =>
          new Property(
            property.id,
            property.place,
            property.noOfBedroom,
            property.noOfBathroom,
            property.sizeinSqft,
            property.nearbySchool,
            property.nearbyCollege,
            property.nearbyHospital,
            property.description,
            property.price,
            property.lastUpdated
          )
      );
    } catch (error) {
      console.error(`PropertyService :: getAllProperties :: Error: ${error}`);
      throw new Error("Failed to fatch property data!");
    }
  }

  // get property details
  async getPropertyDetails(id: string): Promise<Property> {
    try {
      const result = await axios.get(`${this.apiUrl}/${id}`);
      return result.data;
    } catch (error) {
      console.error(`PropertyService :: getPropertyDetails :: Error: ${error}`);
      throw new Error("Failed to fatch property data!");
    }
  }
}

const propertyService = new PropertyService();

export default propertyService;
