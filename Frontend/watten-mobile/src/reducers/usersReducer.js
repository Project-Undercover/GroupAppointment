import { FETCH_USERS_SUCCESS, FILTER_USERS } from "../constants/actionTypes";
const initialState = {
  users: [],
  searchedUsers: [],
  error: null,
};

const usersReducer = (state = initialState, action) => {
  switch (action.type) {
    case FETCH_USERS_SUCCESS:
      return {
        ...state,
        users: action.payload,
        searchedUsers: action.payload,
      };

    case FILTER_USERS:
      const searchInput = action.payload.toLowerCase();
      const filteredUsers = state.users.filter((user) => {
        const { mobileNumber, firstName, lastName } = user;
        return (
          !searchInput ||
          mobileNumber.includes(searchInput) ||
          firstName.includes(searchInput) ||
          lastName.includes(searchInput)
        );
      });
      return {
        ...state,
        searchedUsers: filteredUsers,
      };

    default:
      return state;
  }
};

export default usersReducer;
