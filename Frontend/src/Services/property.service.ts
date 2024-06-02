import axios from "axios";
import appConfig from "@/config/appConfig";
import { ISP } from "@/Models/isp";

class PropertyService {
  apiUrl = appConfig.BaseUrl + "/property";

  // get all isp details
  async getAllIsp(): Promise<ISP[]> {
    try {
      const result = await axios.get(this.apiUrl);
      return result.data.map(
        (isp: ISP) =>
          new ISP(
            isp.id,
            isp.place,
            isp.noOfBedroom,
            isp.noOfBathroom,
            isp.sizeinSqft,
            isp.nearbySchool,
            isp.nearbyCollege,
            isp.nearbyHospital,
            isp.description,
            isp.price,
            isp.lastUpdated
          )
      );
    } catch (error) {
      console.error(`IspService :: getAllIsp :: Error: ${error}`);
      throw new Error("Failed to fatch isp data!");
    }
  }

  // get isp details
  async getIspDetails(id: string): Promise<ISP> {
    try {
      const result = await axios.get(`${this.apiUrl}/${id}`);
      return result.data;
    } catch (error) {
      console.error(`IspService :: getIspDetails :: Error: ${error}`);
      throw new Error("Failed to fatch isp data!");
    }
  }
}

const propertyService = new PropertyService();

export default propertyService;
