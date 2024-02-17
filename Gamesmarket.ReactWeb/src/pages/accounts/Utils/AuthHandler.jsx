export function isAdminOrModerator() {
  const role = localStorage.getItem("role");
  return role === "Administrator" || role === "Moderator";
}
export function isAdmin() {
  const role = localStorage.getItem("role");
  return role === "Administrator";
}
export function isAuthenticated() {
  const token = localStorage.getItem("token");
  return token !== null && token !== undefined;
}
