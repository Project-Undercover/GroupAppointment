import {
  StyleSheet,
  TouchableOpacity,
  View,
  ScrollView,
  LayoutAnimation,
  Switch,
} from "react-native";
import { useEffect, useMemo, useState } from "react";
import { Mode } from "../../../utils/Enums";
import DefaultHeader from "../../components/DefaultHeader";
import CustomeStatusBar from "../../components/CustomeStatusBar";
import DefaultInput from "../../components/DefaultInput";
import { Feather, AntDesign } from "@expo/vector-icons";
import theme from "../../../utils/theme";
import Spacer from "../../components/Spacer";
import TextComponent from "../../components/TextComponent";
import InputItem from "./components/InputItem";
import { useTranslation } from "react-i18next";
import DefaultButton from "../../components/DefaultButton";
import UserActions from "../../../actions/UserActions";
import { useDispatch, useSelector } from "react-redux";
import { useForm, Controller } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { validationUser } from "../../../utils/validations/validationSchema";
import globalStyles from "../../../utils/theme/globalStyles";
import { windowWidth } from "../../../utils/dimensions";
import CustomSwitch from "../../components/CustomSwitch";
import DropDownPicker from "react-native-dropdown-picker";
const UserManager = ({ route }) => {
  const { roles } = useSelector((state) => state.users);
  const { mode, user } = route.params;
  const [openRolesSelector, setOpenRolesSelector] = useState(false);

  const [selectedRole, setSelectRole] = useState(0);
  const [isActive, setIsActive] = useState(false);
  const [inputItems, setInputItems] = useState([]);
  const dispatch = useDispatch();
  const userActions = UserActions();

  const {
    handleSubmit,
    control,
    reset,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(validationUser),

    defaultValues: useMemo(() => {
      return {
        firstName: user?.firstName,
        lastName: user?.lastName,
        mobileNumber: user?.mobileNumber,
      };
    }, [user]),
  });
  const { t } = useTranslation();

  const ManagerTitle = useMemo(() => {
    return mode === Mode.Add ? t("create_user") : t("edit_user");
  }, [mode]);

  const handlePressAddChild = () => {
    const newItem = {
      id: Date.now(),
      isNew: true,
    };
    setInputItems([...inputItems, newItem]);

    LayoutAnimation.configureNext(LayoutAnimation.Presets.easeInEaseOut);
  };

  const handleRemoveInput = (id) => {
    setInputItems((prev) => prev.filter((item) => item.id !== id));
  };
  const handleChangeInput = (id, value) => {
    setInputItems((prevItems) =>
      prevItems?.map((item) =>
        item.id === id ? { ...item, name: value } : item
      )
    );
  };

  useEffect(() => {
    if (!user) return;
    setInputItems(user?.children);
    setIsActive(user?.isActive);
    setSelectRole(user?.role);
  }, [user]);

  const handleCreateUser = (data) => {
    const { firstName, lastName, mobileNumber } = data;
    const children = inputItems.map((item) => item?.name);
    dispatch(
      userActions.createUser({ firstName, lastName, mobileNumber, children })
    );
  };

  const handleUpdateUser = (data) => {
    const { firstName, lastName, mobileNumber } = data;
    const _inputChildren = [...inputItems];
    const children = _inputChildren?.map((item) => {
      const child = item?.isNew ? { name: item?.name } : { ...item };
      return child;
    });
    dispatch(
      userActions.editUser({
        id: user?.id,
        firstName,
        lastName,
        mobileNumber,
        children,
        isActive,
        role: selectedRole,
      })
    );
  };

  const ConfrimFunction = useMemo(() => {
    return mode === Mode.Add ? handleCreateUser : handleUpdateUser;
  }, [mode, inputItems, isActive, selectedRole]);

  const IsAddMode = useMemo(() => {
    return mode === Mode.Add;
  }, [mode]);
  return (
    <View className="flex-1">
      <CustomeStatusBar />
      <DefaultHeader title={ManagerTitle} />
      <ScrollView
        className="flex-1"
        contentContainerStyle={{
          paddingBottom: 120,
          alignItems: "left",
          flexGrow: 1,
          padding: 14,
        }}
      >
        {!IsAddMode ? (
          <CustomSwitch
            value={isActive}
            label={t("user_status")}
            onValueChange={() => setIsActive((prev) => !prev)}
          />
        ) : null}
        <Spacer space={20} />
        <View
          className="flex-row  justify-between"
          style={{ width: windowWidth * 0.92 }}
        >
          <View>
            <Controller
              control={control}
              rules={{ required: true }}
              render={({ field: { onChange, value, onBlur } }) => (
                <DefaultInput
                  placeholder={t("enter") + " " + t("firstName")}
                  label={t("firstName")}
                  onChange={onChange}
                  value={value}
                  wrapperStyle={{ width: windowWidth * 0.44 }}
                  icon={
                    <Feather
                      name="user"
                      color={theme.COLORS.primary}
                      size={20}
                    />
                  }
                />
              )}
              name="firstName"
              defaultValue={""}
            />
            {errors?.firstName && (
              <TextComponent style={globalStyles.errorText}>
                {t(errors.firstName.message)}
              </TextComponent>
            )}
          </View>
          <Spacer space={3} />

          <View>
            <Controller
              control={control}
              rules={{ required: true }}
              render={({ field: { onChange, value, onBlur } }) => (
                <DefaultInput
                  placeholder={t("enter") + " " + t("lastName")}
                  label={t("lastName")}
                  value={value}
                  onChange={onChange}
                  wrapperStyle={{ width: windowWidth * 0.44 }}
                  // wrapperStyle={{ width: "50%" }}
                  icon={
                    <Feather
                      name="user"
                      color={theme.COLORS.primary}
                      size={20}
                    />
                  }
                />
              )}
              name="lastName"
              defaultValue={""}
            />
            {errors.lastName && (
              <TextComponent style={globalStyles.errorText}>
                {t(errors.lastName.message)}
              </TextComponent>
            )}
          </View>
        </View>

        <Spacer space={15} />

        <Controller
          control={control}
          rules={{ required: true }}
          render={({ field: { onChange, value, onBlur } }) => (
            <DefaultInput
              placeholder={t("enter") + " " + t("phoneNumber")}
              onChange={onChange}
              keyboardType="numeric"
              value={value}
              label={t("phoneNumber")}
              wrapperStyle={{ width: "100%" }}
              icon={
                <Feather name="phone" color={theme.COLORS.primary} size={20} />
              }
            />
          )}
          name="mobileNumber"
          defaultValue={""}
        />
        {errors.mobileNumber && (
          <TextComponent style={globalStyles.errorText}>
            {t(errors.mobileNumber.message)}
          </TextComponent>
        )}
        <Spacer space={15} />
        <View style={styles.dropContainer}>
          <TextComponent style={styles.label}>
            {t("session_instructure")}
          </TextComponent>
          <DropDownPicker
            open={openRolesSelector}
            value={selectedRole}
            listMode="SCROLLVIEW"
            setOpen={setOpenRolesSelector}
            setValue={setSelectRole}
            items={roles?.map((role) => ({
              label: role?.name,
              value: role?.value,
            }))}
            stickyHeader
            dropDownContainerStyle={{
              borderColor: theme.COLORS.secondary2,
              padding: 10,
              zIndex: 999,
            }}
            style={globalStyles.dropDownInput}
            labelStyle={{ textAlign: "left" }}
            maxHeight={300}
            mode="BADGE"
            placeholderStyle={{
              color: theme.COLORS.gray1,
              fontSize: 14,
              fontFamily: theme.FONTS.primaryFontRegular,
              textAlign: "left",
            }}
            badgeDotColors={[theme.COLORS.primary]}
          />
        </View>

        <Spacer space={15} />

        <View className="flex-1 gap-[10px]">
          <TouchableOpacity
            className="flex-row items-center gap-2"
            onPress={handlePressAddChild}
          >
            <AntDesign
              name="plussquare"
              size={26}
              color={theme.COLORS.primary}
            />
            <TextComponent style={styles.addText}>
              {t("add_child")}
            </TextComponent>
          </TouchableOpacity>
          <Spacer space={15} />
          {inputItems?.map((item) => (
            <InputItem
              key={item.id}
              id={item.id}
              t={t}
              value={item?.name}
              handleRemoveInput={() => handleRemoveInput(item.id)}
              handleChangeInput={handleChangeInput}
            />
          ))}
        </View>
        <Spacer space={20} />
        <View className="items-center w-full justify-end">
          <DefaultButton
            text={ManagerTitle}
            onPress={handleSubmit(ConfrimFunction)}
          />
        </View>
      </ScrollView>
    </View>
  );
};

export default UserManager;

const styles = StyleSheet.create({
  addText: {
    fontSize: 17,
  },
  label: {
    fontSize: 15,
    color: theme.COLORS.secondaryPrimary,
    paddingStart: 15,
    marginBottom: 5,
  },
  dropContainer: {
    zIndex: 2,
  },
});
