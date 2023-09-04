// import React, {
//   useState,
//   useEffect,
//   useRef,
//   useCallback,
//   useMemo,
// } from "react";
// import { View, Text, TextInput, Button, StyleSheet } from "react-native";
// import MapView, { Marker } from "react-native-maps";
// import * as Location from "expo-location";
// import { GooglePlacesAutocomplete } from "react-native-google-places-autocomplete";
// import BottomSheet, { BottomSheetBackdrop } from "@gorhom/bottom-sheet";
// import theme from "../../../../utils/theme";
// export default function LocationPickerScreen() {
//   const [selectedLocation, setSelectedLocation] = useState(null);
//   const [sessionLocation, setSessionLocation] = useState("");
//   const [initialRegion, setInitialRegion] = useState({
//     latitude: 31.771959, // Israel's approximate latitude
//     longitude: 35.217018, // Israel's approximate longitude
//     latitudeDelta: 1.5, // Adjust the zoom level as needed
//     longitudeDelta: 1.5, // Adjust the zoom level as needed
//   });
//   const bottomSheetModalRef = useRef(null);

//   const snapPoints = useMemo(() => ["45%", "85%", "100%"], []);

//   const handlePresentModalPress = useCallback(() => {
//     bottomSheetModalRef.current?.present();
//   }, []);

//   const handleSheetChanges = useCallback((index) => {}, []);

//   // Request location permission and get the current location
//   useEffect(() => {
//     const getLocation = async () => {
//       const { status } = await Location.requestForegroundPermissionsAsync();
//       if (status !== "granted") {
//         console.error("Location permission denied");
//         return;
//       }

//       const location = await Location.getCurrentPositionAsync({});
//       const currentLatitude = location.coords.latitude;
//       const currentLongitude = location.coords.longitude;

//       // Set the initial region to the user's current location
//       setInitialRegion({
//         latitude: currentLatitude,
//         longitude: currentLongitude,
//         latitudeDelta: 0.0922,
//         longitudeDelta: 0.0421,
//       });
//     };

//     getLocation();
//   }, []);

//   // Handle map press event
//   const handleMapPress = (event) => {
//     const { latitude, longitude } = event.nativeEvent.coordinate;
//     setSelectedLocation({ latitude, longitude });
//   };

//   // Save the selected location and close the map
//   const confirmLocation = () => {
//     if (selectedLocation) {
//       setSessionLocation(
//         `Latitude: ${selectedLocation.latitude}, Longitude: ${selectedLocation.longitude}`
//       );
//     }
//     // Close the map or navigate back to the previous screen
//   };

//   return (
//     <BottomSheet
//       backdropComponent={(backdropProps) => (
//         <BottomSheetBackdrop {...backdropProps} enableTouchThrough={true} />
//       )}
//       ref={bottomSheetModalRef}
//       index={1} // Start with the first snap point
//       snapPoints={snapPoints}
//       //   onClose={handleShowSheet}
//       enablePanDownToClose
//       onChange={handleSheetChanges}
//     >
//       <View style={{ flex: 1 }}>
//         <GooglePlacesAutocomplete
//           placeholder="Search for a location"
//           onPress={(data, details) => {
//             // 'data' contains the selected place data
//             // 'details' contains additional information about the place
//             const { geometry } = details;
//             setSelectedLocation({
//               latitude: geometry.location.lat,
//               longitude: geometry.location.lng,
//             });
//           }}
//           query={{
//             key: "YOUR_GOOGLE_PLACES_API_KEY",
//             language: "en", // You can set the language as needed
//           }}
//           listViewDisplayed="auto"
//           fetchDetails={true}
//           styles={{
//             container: {
//               position: "absolute",
//               top: 10,
//               left: 10,
//               right: 10,
//             },
//             textInputContainer: {
//               width: "100%",
//             },
//             description: {
//               fontWeight: "bold",
//             },
//             predefinedPlacesDescription: {
//               color: "#1faadb",
//             },
//           }}
//         />

//         <MapView
//           style={{ flex: 1 }}
//           initialRegion={initialRegion}
//           onPress={handleMapPress}
//         >
//           {selectedLocation && (
//             <Marker coordinate={selectedLocation} title="Selected Location" />
//           )}
//         </MapView>

//         <TextInput
//           placeholder="Session Location"
//           value={sessionLocation}
//           onChangeText={(text) => setSessionLocation(text)}
//         />

//         <Button title="Confirm Location" onPress={confirmLocation} />
//       </View>
//     </BottomSheet>
//   );
// }
// const styles = StyleSheet.create({
//   container: {
//     flex: 1,
//     backgroundColor: "rgba(0, 0, 0, 0.1)",
//     justifyContent: "center",
//     alignItems: "center",
//   },
//   contentContainer: {
//     flex: 1,
//     padding: 10,
//   },
//   text: {
//     color: theme.COLORS.black,
//     fontSize: 15,
//   },
// });
import React, {
  useState,
  useEffect,
  useRef,
  useCallback,
  useMemo,
} from "react";
import { View, Text, TextInput, Button, StyleSheet } from "react-native";
import MapView, { Marker } from "react-native-maps";
import * as Location from "expo-location";
import { GooglePlacesAutocomplete } from "react-native-google-places-autocomplete";
import BottomSheet, { BottomSheetBackdrop } from "@gorhom/bottom-sheet";
import theme from "../../../../utils/theme";

export default function LocationPickerScreen() {
  const [selectedLocation, setSelectedLocation] = useState(null);
  const [sessionLocation, setSessionLocation] = useState("");
  const [initialRegion, setInitialRegion] = useState({
    latitude: 31.771959, // Israel's approximate latitude
    longitude: 35.217018, // Israel's approximate longitude
    latitudeDelta: 1.5, // Adjust the zoom level as needed
    longitudeDelta: 1.5, // Adjust the zoom level as needed
  });
  const bottomSheetModalRef = useRef(null);

  const snapPoints = useMemo(() => ["45%", "85%", "100%"], []);

  const handlePresentModalPress = useCallback(() => {
    bottomSheetModalRef.current?.present();
  }, []);

  const handleSheetChanges = useCallback((index) => {}, []);

  // Request location permission and get the current location
  useEffect(() => {
    const getLocation = async () => {
      const { status } = await Location.requestForegroundPermissionsAsync();
      if (status !== "granted") {
        console.error("Location permission denied");
        return;
      }

      const location = await Location.getCurrentPositionAsync({});
      const currentLatitude = location.coords.latitude;
      const currentLongitude = location.coords.longitude;

      // Set the initial region to the user's current location
      const updatedRegion = {
        latitude: currentLatitude,
        longitude: currentLongitude,
        latitudeDelta: 0.0922, // Adjust the zoom level as needed
        longitudeDelta: 0.0421, // Adjust the zoom level as needed
      };
      setInitialRegion(updatedRegion);
    };

    getLocation();
  }, []);

  // Handle map press event
  const handleMapPress = (event) => {
    const { latitude, longitude } = event.nativeEvent.coordinate;
    setSelectedLocation({ latitude, longitude });
  };

  // Save the selected location and close the map
  const confirmLocation = () => {
    if (selectedLocation) {
      setSessionLocation(
        `Latitude: ${selectedLocation.latitude}, Longitude: ${selectedLocation.longitude}`
      );
    }
    // Close the map or navigate back to the previous screen
  };

  return (
    <BottomSheet
      backdropComponent={(backdropProps) => (
        <BottomSheetBackdrop {...backdropProps} enableTouchThrough={true} />
      )}
      ref={bottomSheetModalRef}
      index={1} // Start with the first snap point
      snapPoints={snapPoints}
      //   onClose={handleShowSheet}
      enablePanDownToClose
      onChange={handleSheetChanges}
    >
      <View style={{ flex: 1 }}>
        <View
          style={{
            position: "absolute",
            top: 0,
            borderWidth: 2,
            width: "100%",
            height: 500,
          }}
        >
          <GooglePlacesAutocomplete
            placeholder="Search"
            query={{
              key: "4Z[fV&GPIzx6fV&t#4=!_R:I$vZXUJupa}_VbF#-q/%.^8E5t6-Y5'%|F}Vu^25#48[ttoN.'~eC|{?P(bc8",
              language: "en",
            }}
            onPress={(data, details = null) => console.log(data)}
            onFail={(error) => console.error(error)}
            requestUrl={{
              url: "https://cors-anywhere.herokuapp.com/https://maps.googleapis.com/maps/api",
              useOnPlatform: "web",
            }} // this in only required for use on the web. See https://git.io/JflFv more for details.
          />
        </View>
        {/* <MapView
          style={{ flex: 1 }}
          initialRegion={initialRegion}
          onPress={handleMapPress}
        >
          {selectedLocation && (
            <Marker coordinate={selectedLocation} title="Selected Location" />
          )}
        </MapView> */}

        <TextInput
          placeholder="Session Location"
          value={sessionLocation}
          onChangeText={(text) => setSessionLocation(text)}
        />

        {/* <Button title="Confirm Location" onPress={confirmLocation} /> */}
      </View>
    </BottomSheet>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "rgba(0, 0, 0, 0.1)",
    justifyContent: "center",
    alignItems: "center",
  },
  contentContainer: {
    flex: 1,
    padding: 10,
  },
  text: {
    color: theme.COLORS.black,
    fontSize: 15,
  },
});
