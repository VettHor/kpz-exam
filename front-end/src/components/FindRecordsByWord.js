import { DatePicker, DocumentCard, Dropdown, IDropdownOption, PrimaryButton, Stack, TextField } from "@fluentui/react";
import { useEffect, useState } from "react";
import { API_URL } from "./TherapistTable";

export const FindRecordsByWord = () => {
  const [records, setRecords] = useState(null);
  const [word, setWord] = useState('');

  const FindRecordsByWord = (word) => {
    fetch(`${API_URL}/Record/byword/${word}`)
      .then(response => response.json())
      .then(response => {
        setRecords(response);
      })
  }

  useEffect(() => {
    if(word !== null && word !== '')
    FindRecordsByWord(word);
  }, [word]);

  return (
      <Stack>
        <DocumentCard>
          <TextField onChange={(e, v) => { setWord(v) }} label="Enter word"></TextField>
          <table>
            <thead>
              <th>Text</th>
              <th>Time</th>
              <th>Frequency</th>
            </thead>
            <tbody> {
                records?.map(record =>
                  <tr>
                    <td>{record.text}</td>
                    <td>{record.visitTime}</td>
                    <td>{record.frequency}</td>
                  </tr>
                )
              }
            </tbody>
          </table>
        </DocumentCard>
      </Stack>
  );
}