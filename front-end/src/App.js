import { Route, Routes, BrowserRouter } from 'react-router-dom';
import './App.css';
import { Home } from './components/Home'
import { TherapistTable } from './components/TherapistTable';
import { AddRecordForm } from './components/AddRecordForm'
import { FindRecordsByWord } from './components/FindRecordsByWord';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<Home />}></Route>
        <Route path='/therapists' element={<TherapistTable />}></Route>
        <Route path='/add-record' element={<AddRecordForm />}></Route>
        <Route path='/find-records' element={<FindRecordsByWord />}></Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
