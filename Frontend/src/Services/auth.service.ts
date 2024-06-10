import axios from "axios";
import appConfig from "@/config/appConfig";
import { AuthToken, AuthUser, LoginRequest } from "@/Models/auth";

class AuthService {
  apiUrl = appConfig.BaseUrl + "/auth";

  async login(loginData: LoginRequest): Promise<AuthToken> {
    try {
      const result = await axios.post(this.apiUrl + "/login", loginData);
      return result.data;
    } catch (error) {
      console.error(`AuthService :: login :: Error: ${error}`);
      throw new Error("Login Failed");
    }
  }

  async register(registerData: any) {
    try {
      const result = await axios.post(this.apiUrl + "/register", registerData);
      return result.data;
    } catch (error) {
      console.error(`AuthService :: register :: Error: ${error}`);
      throw error;
    }
  }

  async verify(token: string): Promise<AuthUser> {
    try {
      const config = {
        headers: { Authorization: `Bearer ${token}` },
      };

      const result = await axios.get(this.apiUrl + "/verify", config);
      return result.data;
    } catch (error) {
      console.error(`AuthService :: verify :: Error: ${error}`);
      throw error;
    }
  }
}

const authService = new AuthService();

export default authService;
