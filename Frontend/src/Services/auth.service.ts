import appConfig from "@/config/appConfig";
import axios from "axios";

class AuthService {
  apiUrl = appConfig.BaseUrl + "/auth";

  async login(loginData: any) {
    try {
      const result = await axios.post(this.apiUrl + "/login", loginData);
      return result.data;
    } catch (error) {
      console.error(`AuthService :: getAllIsp :: Error: ${error}`);
      throw new Error("Failed to fatch isp data!");
    }
  }

  async register(registerData: any) {
    try {
      const result = await axios.post(this.apiUrl + "/register", registerData);
      return result.data;
    } catch (error) {
      console.error(`AuthService :: getAllIsp :: Error: ${error}`);
      throw new Error("Failed to fatch isp data!");
    }
  }

  async verify(token: any) {
    try {
      const config = {
        headers: { Authorization: `Bearer ${token}` },
      };

      const result = await axios.get(this.apiUrl + "/verify", config);
      return result.data;
    } catch (error) {
      console.error(`AuthService :: getAllIsp :: Error: ${error}`);
      throw new Error("Failed to fatch isp data!");
    }
  }
}

const authService = new AuthService();

export default authService;
