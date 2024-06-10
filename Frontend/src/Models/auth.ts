export interface AuthUser {
  userName: string;
  email: string;
  userId: string;
  role: string;
}

export interface AuthToken {
  token: string;
  expiration: string;
}

export interface LoginRequest {
  username: string;
  password: string;
}
