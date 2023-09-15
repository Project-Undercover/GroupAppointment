import * as yup from "yup";

export const validationSchema = yup.object().shape({
  title: yup.string().required("validation.sessionTitle"),
  locationName: yup.string().required("validation.sessionLocation"),
  maxParticipants: yup
    .string()
    .required("validation.maxPart.required")
    .matches(/^[0-9]+$/, "validation.maxPart.matches"),
});
export const validationUser = yup.object().shape({
  firstName: yup.string().required("validation.firstName"),
  lastName: yup.string().required("validation.lastName"),
  mobileNumber: yup
    .string()
    .required("validation.mobileNumber.required")
    .matches(/^[0-9]+$/, "validation.mobileNumber.matches")
    .min(10, "validation.mobile_ten_digits_required")
    .max(10, "validation.mobile_ten_digits_required"),
});
