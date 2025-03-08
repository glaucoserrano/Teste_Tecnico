import {CssBaseline} from '@mui/material'
import './App.css'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import Sidebar from './components/Sidebar'
import Usuarios from './pages/Usuarios'
import Restaurantes from './pages/Restaurante'
import Voto from './pages/Votos'
import Home from './pages/Home'

export default function App() {
  return (
    <>
    <CssBaseline />
      <BrowserRouter>
        <Sidebar>
          <Routes>
            <Route path="/" element ={<Home />}/>
            <Route path="/usuarios" element={<Usuarios />} />
            <Route path="/restaurantes" element={<Restaurantes />} />
            <Route path="/votacao" element={<Voto />} />
          </Routes>
        </Sidebar>
      </BrowserRouter>
    </>
  )
}