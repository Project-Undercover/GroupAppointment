import { NavigationContainer, useNavigation } from "@react-navigation/native";
import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import { createStackNavigator } from "@react-navigation/stack";
import { createDrawerNavigator } from "@react-navigation/drawer";
import BottomBar from "../presentation/components/BottomBar";

import Home from "../presentation/screens/home/Home";
import Profile from "../presentation/screens/profile/Profile";
import UserManager from "../presentation/screens/user-manager/UserManager";
import Sessions from "../presentation/screens/sessions/Sessions";
import Entry from "../presentation/screens/entry/Entry";
import SessionManager from "../presentation/screens/SessionManager/SessionManager";
import Users from "../presentation/screens/users/Users";
import Splash from "../presentation/screens/splash/Splash";
import History from "../presentation/screens/history/History";
import DrawerContent from "../presentation/components/DrawerContent";

const Stack = createStackNavigator();
const SessionStack = createStackNavigator();
const UserStack = createStackNavigator();
const AppTabs = createBottomTabNavigator();
const Drawer = createDrawerNavigator();

export const SessionsStack = () => {
  return (
    <SessionStack.Navigator
      tabBarOptions={{
        gestureEnabled: false,
      }}
      screenOptions={{
        headerShown: false,
      }}
      initialRouteName={"sessions"}
    >
      <SessionStack.Screen name={"sessions"} component={Sessions} />
      <SessionStack.Screen
        name={"session-manager"}
        component={SessionManager}
      />
    </SessionStack.Navigator>
  );
};

export const UsersStack = () => {
  return (
    <UserStack.Navigator
      tabBarOptions={{
        keyboardHidesTabBar: true,
        gestureEnabled: false,
      }}
      screenOptions={{
        headerShown: false,
      }}
      initialRouteName={"users"}
    >
      <UserStack.Screen name={"users"} component={Users} />
      <UserStack.Screen name={"user-manager"} component={UserManager} />
    </UserStack.Navigator>
  );
};

const AppBottomTabs = () => {
  return (
    <AppTabs.Navigator
      initialRouteName={"home"}
      screenOptions={{
        headerShown: false,
        tabBarShowLabel: false,
        gestureEnabled: false,
      }}
      tabBar={(props) => <BottomBar {...props} />}
    >
      <AppTabs.Screen name={"home"} component={Home} />
      <AppTabs.Screen name={"sessionsStack"} component={SessionsStack} />
      <AppTabs.Screen name={"usersStack"} component={UsersStack} />
      <AppTabs.Screen name={"profile"} component={Profile} />
    </AppTabs.Navigator>
  );
};

const AppDrawer = () => {
  return (
    <Drawer.Navigator
      initialRouteName={"app-tabs"}
      screenOptions={{
        headerShown: false,
        gestureEnabled: false,
      }}
      drawerContent={(props) => <DrawerContent {...props} />}
    >
      <Drawer.Screen name={"app-tabs"} component={AppBottomTabs} />
      <Drawer.Screen name="history" component={History} />
    </Drawer.Navigator>
  );
};

const AppNavigation = () => {
  return (
    <Stack.Navigator
      initialRouteName={"splash"}
      screenOptions={{
        headerShown: false,
        gestureEnabled: false,
      }}
    >
      <Stack.Screen
        name={"splash"}
        component={Splash}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name={"app-drawer"}
        component={AppDrawer}
        options={{ headerShown: false }}
      />
      <Stack.Screen name="entry" component={Entry} />
    </Stack.Navigator>
  );
};

const Navigation = () => {
  return (
    <NavigationContainer>
      <AppNavigation />
    </NavigationContainer>
  );
};
export default Navigation;
