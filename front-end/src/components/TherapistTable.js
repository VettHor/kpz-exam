import { DefaultButton, DocumentCard, Dropdown, Stack, TextField } from "@fluentui/react";
import React, { useEffect, useState } from 'react';
export const API_URL = "https://localhost:7207/api";

export const TherapistTable = () => {
  const [therapists, setTherapists] = useState([]);
  const [records, setRecords] = useState([]);
  const [selectedTherapistCalendar, setSelectedTherapistCalendar] = useState(null);
  const [selectedTherapist, setSelectedTherapist]  = useState('');
  const [currViewTherapist, setCurrViewTherapist]  = useState('');

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

  const deleteTherapist = (id) => {
    fetch(`${API_URL}/Therapist/${id}`, {
      method: "DELETE"
    }).then(res => console.log(res.status))
    getAllTherapists();
  }

  const getCalendar = (id) => {
    fetch(`${API_URL}/Calendar/therapist/${id}`)
    .then(response => response.json())
    .then(response => {
      response.therapistId = id;
      setSelectedTherapistCalendar(response);
    })
  }

  const items = therapists.map(therapist => {
    return {
        key: therapist.id, 
        text: therapist.name + " " + therapist.surname + " -> " + therapist.receptionRoom
    }
}) 

  const updateRecord = (record) => {
    fetch(`${API_URL}/Record`, {
      method: "PUT",
      headers: {
        'Content-type': 'application/json; charset=UTF-8' 
      },
      body: JSON.stringify(record)
    })
  }

  const deleteRecord = (id) => {
    fetch(`${API_URL}/Record/${id}`, {
      method: "DELETE"
    }).then(res => console.log(res.status))
    records.splice(records.map(function(e) { 
      return e.id; 
    }).indexOf(id));
  }

  const getCurrentCalendar = (id) => {
    fetch(`${API_URL}/Calendar/therapist/${id}`)
    .then(response => response.json())
    .then(response => {
      response.therapistId = id;
      fetch(`${API_URL}/Record/calendar/${response.id}`)
      .then(response => response.json())
      .then(response => setRecords(response))
    });
}

  return (
    <>
      <Stack>
        <DocumentCard>
          <Dropdown onChange={(e,o) => {
            setSelectedTherapist(o.key);
            getCalendar(o.key);
          }} label="Therapist selection" options={items} placeholder="Select therapist"></Dropdown>
        </DocumentCard>
      </Stack>
      <table>
        <thead>
          <th>Name</th>
          <th>Surname</th>
          <th>Reception room</th>
          <th>View therapist's calendar</th>
          <th>Delete</th>
        </thead>
        <tbody>
          {
            therapists?.map(therapist => 
              <tr>
                <td>{therapist.name}</td>
                <td>{therapist.surname}</td>
                <td>{therapist.receptionRoom}</td>
                <td><DefaultButton onClick={() => {
                  getCurrentCalendar(therapist.id);
                  setCurrViewTherapist(therapist.id);
                  }}>View</DefaultButton></td>
                <td><DefaultButton onClick={() => {deleteTherapist(therapist.id)}}>Delete</DefaultButton></td>
              </tr> 
            )
          }
          </tbody>
      </table>

       { selectedTherapistCalendar && selectedTherapist && therapists && currViewTherapist &&
        therapists.find(therapist => therapist.id === currViewTherapist)?.receptionRoom === 
        therapists.find(therapist => therapist.id === selectedTherapist)?.receptionRoom ?
        

        <table>
          <thead>
            <th>Text</th>
            <th>Time</th>
            <th>Frequency</th>
            <th>Save changes</th>
            <th>Delete record</th>
          </thead>
          <tbody> {
              records?.map(record =>
                <tr>
                  <td>
                    <TextField 
                      defaultValue={record.text} 
                      onChange={(e, v) => record.text = v} 
                      disabled={selectedTherapist !== currViewTherapist}
                    />
                  </td>
                  <td>
                    <TextField 
                      defaultValue={record.visitTime} 
                      onChange={(e, v) => record.visitTime = v}
                      disabled={selectedTherapist !== currViewTherapist}
                    />
                  </td>
                  <td>
                    <TextField 
                      defaultValue={record.frequency} 
                      onChange={(e, v) => record.frequency = v}
                      disabled={selectedTherapist !== currViewTherapist}
                    />
                  </td>
                  <td>
                    <DefaultButton 
                      onClick={() => {updateRecord(record.id)}}
                      disabled={selectedTherapist !== currViewTherapist}>
                        Save
                    </DefaultButton>
                  </td>
                  <td>
                    <DefaultButton 
                      onClick={() => {deleteRecord(record)}} 
                      disabled={selectedTherapist !== currViewTherapist}>
                        Delete
                    </DefaultButton>
                  </td>
                </tr>
              )
            }
            </tbody>
          </table> : <h1>You don't have permissions!</h1>
      }
    </>
  );
}