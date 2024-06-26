import {Address} from "./address";

export interface UserDetails {
  username: string;
  email: string;
  dob: string | null;
  firstName: string;
  lastName: string;
  password: string;
  userType: number;
  address: Address | null;
}
