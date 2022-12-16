import { PrimaryButton, Stack } from "@fluentui/react";
import { useNavigate } from "react-router-dom";

export const Home = () => {

  const navigate = useNavigate();

  return (
    <Stack>
        <PrimaryButton onClick={() => { navigate('therapists') }}>View Therapists With Calendar</PrimaryButton>
        <PrimaryButton onClick={() => { navigate('add-record') }}>Add Record</PrimaryButton>
        <PrimaryButton onClick={() => { navigate('find-records') }}>Find Records By Word</PrimaryButton>
    </Stack>
  );
}