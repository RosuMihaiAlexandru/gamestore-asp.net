export const validatePassword = (password: string): string | null => {
  if (password.length < 6) return "Password must be at least 6 characters long";
  if (!/[A-Z]/.test(password))
    return "Password must contain at least one capital letter";
  if (!/[a-z]/.test(password))
    return "Password must contain at least one small letter";
  if (!/[0-9]/.test(password))
    return "Password must contain at least one number";
  if (!/[!@#$%^&*]/.test(password))
    return "Password must contain at least one special character";
  return null;
};
