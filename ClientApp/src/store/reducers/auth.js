import * as actionTypes from "../actions/actionTypes";
import { updateObject } from "../utility";
import Cookies from "universal-cookie";

const cookies = new Cookies();

const initialState = {
	userId: null,
	userName: null,
	email: null,
	isAuthenticated: false,
};

export const clearCookies = () => {
	cookies.remove("accessToken");
	cookies.remove("refreshToken");
	cookies.remove("tokenType");
};

const initAuthData = (state, action) => {
	return updateObject(state, { isAuthenticated: Boolean(cookies.get("accessToken")) });
};

const setAuthData = (state, action) => {
	clearCookies();
	const expires = new Date(new Date().getTime() + 10 * 365 * 24 * 60 * 60 * 1000);

	cookies.set("accessToken", action.authData.access_token, { expires, path: "/" });
	cookies.set("refreshToken", action.authData.refresh_token, { expires, path: "/" });
	cookies.set("tokenType", action.authData.token_type, { expires, path: "/" });

	return updateObject(state, {
		isAuthenticated: true,
		userId: action.authData.user.id,
		userName: action.authData.user.name,
		email: action.authData.user.email,
	});
};

const logout = (state, action) => {
	clearCookies();
	return initialState;
};

const reducer = (state = initialState, action) => {
	switch (action.type) {
		case actionTypes.SET_AUTH_DATA:
			return setAuthData(state, action);
		case actionTypes.LOG_OUT:
			return logout(state, action);
		case actionTypes.INIT_AUTH_DATA:
			return initAuthData(state, action);
		default:
			return state;
	}
};

export default reducer;
