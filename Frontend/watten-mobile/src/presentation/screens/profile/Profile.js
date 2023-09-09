import {
  StyleSheet,
  Image,
  View,
  TouchableOpacity,
  ScrollView,
  I18nManager,
} from "react-native";
import { useEffect, useState, useCallback } from "react";
import CustomeStatusBar from "../../components/CustomeStatusBar";
import AppHeader from "../../components/AppHeader";
import theme from "../../../utils/theme";
import * as ImagePicker from "expo-image-picker";
import ProfileInfoCard from "./components/ProfileInfoCard";
import ProfileInfoRow from "./components/ProfileInfoRow";
import i18next from "../../../utils/i18n";
import {
  FontAwesome,
  Feather,
  MaterialCommunityIcons,
} from "@expo/vector-icons";
import Spacer from "../../components/Spacer";
import RadioButton from "../../components/RadioButton";
import { Language } from "../../../utils/Enums";
import { useTranslation } from "react-i18next";
import AuthActions from "../../../actions/AuthActions";
import UserActions from "../../../actions/UserActions";
import { useDispatch, useSelector } from "react-redux";
import { useFocusEffect } from "@react-navigation/native";

const Profile = () => {
  const { userProfile } = useSelector((state) => state.users);
  const [image, setImage] = useState();
  const [language, setLanguage] = useState(i18next.language);
  const { t } = useTranslation();
  const authActions = AuthActions();
  const userActions = UserActions();
  const dispatch = useDispatch();

  const selectImage = async () => {
    let result = await ImagePicker.launchImageLibraryAsync({
      mediaTypes: ImagePicker.MediaTypeOptions.Images,
      allowsEditing: true,
      aspect: [1, 1],
      quality: 1,
    });

    if (!result.canceled) {
      setImage(result.assets[0].uri);
    }
  };
  useFocusEffect(
    useCallback(() => {
      fetchProfile();
    }, [])
  );
  useEffect(() => {
    fetchProfile();
  }, []);

  const fetchProfile = () => {
    dispatch(userActions.fetchProfile());
  };
  const handleSelectLangOption = (lang) => {
    authActions.changeLanguage(lang);
    setLanguage(lang);
  };
  return (
    <View className="flex-1">
      <CustomeStatusBar />
      <AppHeader />
      <ScrollView
        showsVerticalScrollIndicator={false}
        className="flex-1"
        contentContainerStyle={{
          paddingBottom: 120,
          alignItems: "center",
          padding: 14,
        }}
      >
        <View>
          <View className="items-center">
            <View style={styles.imageContainer}>
              <Image
                style={styles.image}
                source={require("../../../assets/icons/user.png")}
              />
              <TouchableOpacity
                className="absolute bottom-0  right-0"
                onPress={selectImage}
              >
                <Feather name="edit" size={24} color="black" />
              </TouchableOpacity>
            </View>
          </View>
        </View>
        <Spacer space={10} />
        <View className="w-full">
          <ProfileInfoCard title={t("user_details")}>
            <ProfileInfoRow
              value={userProfile?.firstName + " " + userProfile?.lastName}
              icon={
                <Feather name="user" color={theme.COLORS.primary} size={18} />
              }
            />
            <ProfileInfoRow
              value={userProfile?.mobileNumber}
              icon={
                <Feather name="phone" color={theme.COLORS.primary} size={18} />
              }
            />
          </ProfileInfoCard>
          <Spacer space={10} />
          <ProfileInfoCard title={t("registered_children")}>
            {userProfile?.children?.map((child) => (
              <ProfileInfoRow
                key={child?.id}
                value={child?.name}
                icon={
                  <FontAwesome
                    name="child"
                    color={theme.COLORS.primary}
                    size={18}
                  />
                }
              />
            ))}
          </ProfileInfoCard>
          <Spacer space={10} />
          <ProfileInfoCard title={t("language")}>
            <ProfileInfoRow
              value="العربيه"
              disabled={false}
              onPress={() => handleSelectLangOption(Language.Arabic)}
              selectInput={
                <RadioButton
                  isActive={language === Language.Arabic}
                  // handleSelectOption={handleSelectLangOption}
                  option={Language.Arabic}
                />
              }
              icon={
                <MaterialCommunityIcons
                  name="abjad-arabic"
                  color={theme.COLORS.primary}
                  size={18}
                />
              }
            />

            <ProfileInfoRow
              value="עברית"
              disabled={false}
              onPress={() => handleSelectLangOption(Language.Hebrew)}
              selectInput={
                <RadioButton
                  isActive={language === Language.Hebrew}
                  // handleSelectOption={handleSelectLangOption}
                  option={Language.Hebrew}
                />
              }
              icon={
                <MaterialCommunityIcons
                  name="abjad-hebrew"
                  color={theme.COLORS.primary}
                  size={18}
                />
              }
            />
          </ProfileInfoCard>
        </View>
      </ScrollView>
    </View>
  );
};

export default Profile;

const styles = StyleSheet.create({
  imageContainer: {
    width: 90,
    height: 90,
    borderRadius: 50,
    borderWidth: 2,
    margin: 10,
    borderColor: theme.COLORS.white,
    backgroundColor: theme.COLORS.white,
    position: "relative",
    ...theme.SHADOW.lightShadow,
  },
  image: {
    width: "100%",
    height: "100%",
  },
});
