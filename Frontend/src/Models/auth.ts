export interface AuthUser {
  userName: string;
  email: string;
  userId: string;
  role: AuthRole;
}

export interface AuthToken {
  token: string;
  expiration: string;
}

export interface LoginRequest {
  username: string;
  password: string;
}

export enum AuthRole {
  Seller = "Seller",
  Buyer = "Buyer",
}
