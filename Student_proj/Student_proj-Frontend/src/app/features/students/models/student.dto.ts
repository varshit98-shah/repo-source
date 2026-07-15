export interface StudentDTO {
  id: number;
  name: string;
  email: string;
  address: string;
  phone: string;
  // password is used during creation
  password?: string;
  roleName?: string;
  isDeleted?: boolean;
}
