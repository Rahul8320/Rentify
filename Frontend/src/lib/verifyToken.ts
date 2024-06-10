export function isTokenValid(token: string | null, expiryTime: string | null) {
  // Handle potential errors during token decoding or retrieval
  if (!token || !expiryTime) {
    return false;
  }

  try {
    const parsedExpiry = Date.parse(expiryTime); // Decode base64-encoded payload
    const currentTime = Date.now() / 1000; // Get current time in seconds
    return parsedExpiry > currentTime; // Check if expiry time is in the future
  } catch (error) {
    console.error("Error decoding token:", error);
    return false;
  }
}
