import { Alert, Button, Container, FormControl, InputLabel, MenuItem, Select, Snackbar, TextField, Typography } from "@mui/material"
import axios from "axios"
import { useState } from "react"

interface Usuario {
    id: number,
    nome: string,
    email: string
}
interface Restaurante {
    id: number,
    nome: string,
}

const Voto = () =>{
    const BASEURL = "https://localhost:7205/api/"
    const [email, setEmail] = useState("")
    const [usuario, setUsuario ]= useState<Usuario | null>(null)
    const [restaurantes, setRestaurantes] = useState<Restaurante[]>([])
    const [restauranteSelecionado, setRestauranteSelecionado] = useState("")
    const [openSnackbar, setOpenSnackbar] = useState(false)
    const [mensagem, setMensagem] = useState("")

    const validarEmail = async () =>{
        try{
            
            const response = await axios.get(`${BASEURL}Usuario/email/${email}`)
            setUsuario(response.data.usuario)
            buscarRestaurantes(response.data.usuario.id);
        }catch(error: any){
            if(error.response && error.response.data){
                setMensagem(error.response.data.mensagem)
                
            }else{
                setMensagem("Erro ao validar email")
                
            }
            setOpenSnackbar(true)
        }
    }
    const buscarRestaurantes = async(usuarioid : number) =>{
        try{
            const response = await axios.get(`${BASEURL}Restaurante/disponiveis/${usuarioid}`)
            setRestaurantes(response.data.restaurantes)
        }catch(error :any){
            if(error.response && error.response.data){
                setMensagem(error.response.data.mensagem)
                
            }else{
                setMensagem("Erro ao buscar restaurantes")
                
            }
            setOpenSnackbar(true)
        }
    }
    const votar= async () =>{
        if(!restauranteSelecionado){
            setMensagem("Selecione um restaurante")
            return
        }
        try{
            await axios.post(`${BASEURL}Voto`,{
                usuarioId:usuario?.id,
                restauranteId: restauranteSelecionado
            })
            setMensagem("Voto registrado com sucesso!")
            setOpenSnackbar(true)
            setUsuario(null)
            setRestauranteSelecionado("")
            setEmail("")

        }catch(error : any){
            if(error.response && error.response.data){
                setMensagem(error.response.data.mensagem)
                
            }else{
                setMensagem("Erro ao validar email")
                
            }
            setOpenSnackbar(true)
        }
    }

    return(
        <Container maxWidth="sm" style={{textAlign: "center", marginTop:"2rem"}}>
            <Typography variant="h4" gutterBottom>
                Votação de Restaurantes
            </Typography>
            <TextField
                label="Digite seu e-mail"
                variant="outlined"
                fullWidth
                margin="normal"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
            />
            <Button variant="contained" color="primary" onClick={validarEmail}>
                Validar Email
            </Button>
            {usuario && (
                <>
                <FormControl fullWidth margin="normal">
                    <InputLabel>Selecione um restaurante</InputLabel>
                    <Select
                        value={restauranteSelecionado}
                        onChange={(e) => setRestauranteSelecionado(e.target.value)}
                    >
                        {restaurantes.map((restaurante) => (
                            <MenuItem key={restaurante.id} value={restaurante.id}>
                                {restaurante.nome}
                            </MenuItem>
                        ))}
                    </Select>
                </FormControl>
                <Button variant="contained" color="secondary" onClick={votar}>
                    Votar
                </Button>
                </>
            )}
            <Snackbar 
                open={openSnackbar} 
                autoHideDuration={3000} 
                onClose={() => setOpenSnackbar(false)}
            >
                <Alert
                    severity={mensagem.includes("sucesso") ? "success" : "error"}
                    onClose={() => setOpenSnackbar(false)}
                >
                    {mensagem}
                </Alert>
            </Snackbar>
        </Container>
    )
}
export default Voto