export function isTokenValid(token: string | null, expiryTime: string | null) {
  if (!token || !expiryTime) {
    return false;
  }

  try {
    const parsedExpiry = Date.parse(expiryTime);
    const currentTime = Date.now();
    return parsedExpiry > currentTime;
  } catch (error) {
    console.error("Error decoding token:", error);
    return false;
  }
}
