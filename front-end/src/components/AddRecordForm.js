import { DatePicker, Dropdown, DocumentCard, PrimaryButton, Stack, TextField } from "@fluentui/react";
import { useState, useEffect } from "react";
import { API_URL } from "./TherapistTable";

export const AddRecordForm = () => {
  const [therapists, setTherapists] = useState([]);
  const [selectedTherapist, setSelectedTherapist] = useState("");

  const [text, setText] = useState("");
  const [visitTime, setVisitTime] = useState();
  const [frequency, setFrequency] = useState(0);
  
  const [currRecord, setCurrRecord] = useState(null);

  const getAllTherapists = () => {
    fetch(`${API_URL}/Therapist/all`)
      .then(response => response.json())
      .then(response => {
        let raw = response;
        setTherapists(raw);
      })
  }

  useEffect(() => {
    getAllTherapists();
  }, []);

  const items = therapists.map(therapist => {
    return {
        key: therapist.id, 
        text: therapist.name + " " + therapist.surname + " -> " + therapist.receptionRoom
    }
  }) 

  const saveRecord = () => {
      fetch(`${API_URL}/Calendar/therapist/${selectedTherapist}`)
      .then(response => response.json())
      .then(response => response.id)
      .then(response => {
        setCurrRecord({
          id: crypto.randomUUID(),
          text: text,
          visitTime: visitTime,
          calendarId: response,
          frequency: Number(frequency)
        })
      })    
  }

  const addRecord = (record) => {
    console.log(record)
    fetch(`${API_URL}/Records`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(record)
    })
    .then(res => console.log(res.status)) 
  }

  return (
    <>
      <Stack>
        <DocumentCard>
            <Dropdown onChange={(e,o) => {setSelectedTherapist(o.key) }} label="Therapist selection" options={items} placeholder="Select therapist"></Dropdown>
            <TextField onChange={(e, v) => { setText(v) }} label="Text"></TextField>
            <TextField onChange={(e, v) => { setFrequency(v) }} label="Frequency"></TextField>
            <DatePicker onSelectDate={(d) => { setVisitTime(d) }} label="Visit time"></DatePicker>
            <PrimaryButton onClick={() => { saveRecord() }}>Save data</PrimaryButton>
            <PrimaryButton onClick={() => { addRecord(currRecord) }}>Add Record</PrimaryButton>
        </DocumentCard>
      </Stack>   
    </>
  )
}
