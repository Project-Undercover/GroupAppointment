import {
  StyleSheet,
  View,
  TouchableWithoutFeedback,
  Keyboard,
  ScrollView,
} from "react-native";
import { useState, useMemo, useEffect } from "react";
import DefaultInput from "../../../components/DefaultInput";
import { useForm, Controller } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { Feather, MaterialCommunityIcons, AntDesign } from "@expo/vector-icons";
import theme from "../../../../utils/theme";
import TextComponent from "../../../components/TextComponent";
import Spacer from "../../../components/Spacer";
import DateTimePicker from "@react-native-community/datetimepicker";
import DropDownPicker from "react-native-dropdown-picker";
import DefaultButton from "../../../components/DefaultButton";
import UploadImageCard from "./UploadImageCard";
import { useTranslation } from "react-i18next";
import { windowWidth } from "../../../../utils/dimensions";
import { validationSchema } from "../../../../utils/validations/validationSchema";
import { useDispatch } from "react-redux";
import SessionActions from "../../../../actions/SessionActions";
import moment from "moment";
import globalStyles from "../../../../utils/theme/globalStyles";
import { Mode } from "../../../../utils/Enums";
const Inputs = ({ date, instructors, session, mode }) => {
  const sessionActions = SessionActions();
  const dispatch = useDispatch();
  const { t } = useTranslation();

  const [openInstructorsSelector, setopenInstructorsSelector] = useState(false);
  const [image, setImage] = useState(null);
  const [selectedInstructors, setSelectedInstructors] = useState([]);
  const [startDate, setStartDate] = useState(new Date());
  const [endDate, setEndDate] = useState(new Date());
  const [showTimePicker, setShowTimePicker] = useState(false);
  const [datesError, setDatesError] = useState(false);
  const [enableChangeImage, setEnableSwitchImage] = useState(false);

  const {
    handleSubmit,
    control,
    reset,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(validationSchema),
    defaultValues: useMemo(() => {
      return {
        title: session?.title,
        locationName: session?.locationName,
        maxParticipants: session?.maxParticipants.toString(),
      };
    }, [session]),
  });

  const toggleTimePicker = (ref) => {
    if (showTimePicker) return;
    setShowTimePicker((prev) => !prev);
  };

  const handleChangeStartTime = (event, selectedDate) => {
    setStartDate(selectedDate);
  };
  const handleChangeEndTime = (event, selectedDate) => {
    setEndDate(selectedDate);
  };
  const handleWithoutFeedback = () => {
    if (showTimePicker) {
      setShowTimePicker(false);
    }
    Keyboard.dismiss();
  };

  const isValidDates = (startDate, endDate) => {
    setDatesError(false);

    const sDate = moment(startDate, "YYYY-MM-DDTHH:mm:ss.sssZ");
    const eDate = moment(endDate, "YYYY-MM-DDTHH:mm:ss.sssZ");
    const diffTime = eDate.diff(sDate, "minutes");

    return diffTime > 0;
  };
  const compineDT = (date, time) => {
    let d = moment(date).format("yyyy-MM-DD");
    let t = moment(time).format("HH:mm");
    return moment(d + " " + t).format("YYYY-MM-DDTHH:mm:ss.sssZ");
  };

  const handleCreateSession = (data) => {
    if (!isValidDates(startDate, endDate)) {
      setDatesError(true);
      return;
    }

    const formData = createSessionFormData(data);
    dispatch(sessionActions.createSession(formData));
  };

  const handleEditSession = (data) => {
    if (!isValidDates(startDate, endDate)) {
      setDatesError(true);
      return;
    }

    const formData = createSessionFormData(data);
    dispatch(sessionActions.editSession(formData));
  };
  const createSessionFormData = (data) => {
    const sessionFormData = new FormData();
    if (image && enableChangeImage) {
      const fileUriParts = image.split(".");
      const fileType = fileUriParts[fileUriParts.length - 1];
      sessionFormData.append("imageFile", {
        uri: image,
        name: `imageFileName.${fileType}`,
        type: `image/${fileType}`,
      });
    }
    if (!IsAddMode()) {
      sessionFormData.append("id", session?.id);
      sessionFormData.append("isAvailable", true);
    }
    const _startDate = compineDT(date, startDate);
    const _endDate = compineDT(date, endDate);
    sessionFormData.append("title", data?.title);
    sessionFormData.append("startDate", _startDate);
    sessionFormData.append("endDate", _endDate);
    sessionFormData.append("maxParticipants", data?.maxParticipants);
    sessionFormData.append("locationName", data?.locationName);
    sessionFormData.append("instructors", selectedInstructors);
    return sessionFormData;
  };

  const IsAddMode = () => {
    return mode === Mode.Add;
  };
  const ManagerTitle = () => {
    return IsAddMode() ? "create_session" : "edit_session";
  };
  const ConfrimFunction = () => {
    return IsAddMode() ? handleCreateSession : handleEditSession;
  };

  const handleSelectImage = (image) => {
    setImage(image);
    setEnableSwitchImage(true);
  };

  useEffect(() => {
    if (!session) return;
    const defualtInstructors = session?.instructors?.map(
      (instructor) => instructor?.id
    );

    setStartDate(new Date(session?.startDate));
    setEndDate(new Date(session?.endDate));
    setImage(session?.image);
    setSelectedInstructors(defualtInstructors);
  }, [session]);

  return (
    <TouchableWithoutFeedback onPress={handleWithoutFeedback}>
      <ScrollView
        style={{ flex: 1 }}
        showsVerticalScrollIndicator={false}
        automaticallyAdjustKeyboardInsets={true}
        contentContainerStyle={{ paddingBottom: 120 }}
      >
        <View className="flex-start flex-1" style={{ rowGap: 20 }}>
          <Controller
            control={control}
            rules={{ required: true }}
            render={({ field: { onChange, value, onBlur } }) => (
              <DefaultInput
                placeholder={t("enter") + " " + t("session_title")}
                label={t("session_title")}
                onChange={onChange}
                value={value}
                icon={
                  <MaterialCommunityIcons
                    name="subtitles-outline"
                    color={theme.COLORS.primary}
                    size={20}
                  />
                }
              />
            )}
            name="title"
            defaultValue={""}
          />
          {errors.title && (
            <TextComponent style={globalStyles.errorText}>
              {t(errors.title.message)}
            </TextComponent>
          )}

          <Controller
            control={control}
            rules={{ required: true }}
            render={({ field: { onChange, value, onBlur } }) => (
              <DefaultInput
                placeholder={t("enter") + " " + t("session_address")}
                label={t("session_address")}
                onChange={onChange}
                value={value}
                icon={
                  <Feather name="user" color={theme.COLORS.primary} size={20} />
                }
              />
            )}
            name="locationName"
            defaultValue=""
          />
          {errors?.locationName && (
            <TextComponent style={globalStyles.errorText}>
              {t(errors?.locationName?.message)}
            </TextComponent>
          )}

          <View style={styles.dropContainer}>
            <TextComponent style={styles.label}>
              {t("session_instructure")}
            </TextComponent>
            <DropDownPicker
              open={openInstructorsSelector}
              value={selectedInstructors}
              listMode="SCROLLVIEW"
              setOpen={setopenInstructorsSelector}
              setValue={setSelectedInstructors}
              items={instructors.map((instructor) => ({
                label: instructor?.name,
                value: instructor?.id,
              }))}
              placeholder={t("select")}
              stickyHeader
              dropDownContainerStyle={{
                borderWidth: 1,
                borderColor: theme.COLORS.secondary2,
                padding: 10,
                zIndex: 999,
              }}
              listItemLabelStyle={{
                textAlign: "left",
              }}
              multiple={true}
              style={globalStyles.dropDownInput}
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

          <Controller
            control={control}
            rules={{ required: true }}
            render={({ field: { onChange, value, onBlur } }) => (
              <DefaultInput
                placeholder={t("enter") + " " + t("session_max")}
                label={t("session_max")}
                onChange={onChange}
                value={value}
                keyboardType="numeric"
                icon={
                  <Feather name="user" color={theme.COLORS.primary} size={20} />
                }
              />
            )}
            name="maxParticipants"
            defaultValue=""
          />
          {errors?.maxParticipants && (
            <TextComponent style={globalStyles.errorText}>
              {t(errors?.maxParticipants.message)}
            </TextComponent>
          )}

          <View className="flex-row  justify-between">
            <DefaultInput
              containerStyle={{ width: windowWidth * 0.4 }}
              placeholder={t("enter") + " " + t("session_start")}
              label={t("session_start")}
              // onFocus={toggleTimePicker}
              component={
                <DateTimePicker
                  testID="dateTimePicker"
                  value={startDate}
                  mode={"time"}
                  is24Hour={true}
                  display={"calendar"}
                  onChange={handleChangeStartTime}
                />
              }
              icon={
                <AntDesign
                  name="clockcircleo"
                  color={theme.COLORS.primary}
                  size={18}
                />
              }
            />
            <DefaultInput
              placeholder={t("enter") + " " + t("session_end")}
              label={t("session_end")}
              containerStyle={{ width: windowWidth * 0.4 }}
              component={
                <DateTimePicker
                  testID="dateTimePicker"
                  value={endDate}
                  mode={"time"}
                  is24Hour={true}
                  display={"calendar"}
                  onChange={handleChangeEndTime}
                />
              }
              icon={
                <AntDesign
                  name="clockcircleo"
                  color={theme.COLORS.primary}
                  size={18}
                />
              }
            />
          </View>

          {datesError && (
            <TextComponent style={globalStyles.errorText}>
              {t("date_error")}
            </TextComponent>
          )}
          <UploadImageCard
            handleSelectImage={handleSelectImage}
            image={image}
          />
          <Spacer space={5} />
          <View className="items-center">
            <DefaultButton
              text={t(ManagerTitle())}
              onPress={handleSubmit(ConfrimFunction())}
            />
          </View>
        </View>
      </ScrollView>
    </TouchableWithoutFeedback>
  );
};

export default Inputs;

const styles = StyleSheet.create({
  label: {
    fontSize: 15,
    color: theme.COLORS.secondaryPrimary,
    paddingStart: 15,
    marginBottom: 5,
  },
  input: {
    height: 48,
    flexDirection: "row",
    justifyContent: "center",
    alignItems: "center",
    backgroundColor: "#fff",
    borderWidth: 1,
    borderColor: theme.COLORS.secondary2,
    borderRadius: 5,
    overflow: "hidden",
  },
  dropContainer: {
    zIndex: 2,
  },
  dateContainer: {
    flex: 0.5,
  },
});
