import { Alert, Box, Card, CardContent, Snackbar, Typography } from "@mui/material"
import axios from "axios"
import { useEffect, useState } from "react"

interface Restaurante{
    id:number,
    nome: string
    totalVotos: number
    data?: string
}
interface RestauranteVencedor{
    id:number,
    restauranteNome: string
    totalVotos: number
    data?: string
}

const Home = () =>{
    const BASEURL = "https://localhost:7205/api/"
    const [restauranteHoje,setRestauranteHoje] = useState<Restaurante | null>(null)
    const [vencedoresSemana,setVencedoresSemana] = useState<RestauranteVencedor[]>([])
    const [openSnackbar, setOpenSnackbar] = useState(false)
    const [mensagem, setMensagem] = useState("")

    useEffect(() => {
        const fetchData = async() => {
            try{
                const hojeResponse = await axios.get(`${BASEURL}Voto/resultado`)
                setRestauranteHoje(hojeResponse.data.vencedor)

                const semanaResponse = await axios.get(`${BASEURL}Voto/vencedores-semana`)
                setVencedoresSemana(semanaResponse.data.listaVencedor)
                

            }catch(error: any){
                if(error.response && error.response.data){
                    setMensagem(error.response.data.mensagem)
                    
                }else{
                    setMensagem("Erro ao validar email")
                    
                }
                setOpenSnackbar(true)
            }
        }
        fetchData()
    }, [])
    return(
        <Box sx={{ display:"flex", flexDirection:"column", gap:2, padding:3}}>
            <Card sx={{maxWidth: 600}}>
                <CardContent>
                    <Typography variant="h5" gutterBottom>
                        Restaurante mais votado hoje
                    </Typography>
                    {restauranteHoje ? (
                        <Typography variant="h6">
                            {restauranteHoje.nome} - {restauranteHoje.totalVotos} votos
                        </Typography>
                    ) :(
                        <Typography>Nenhum voto registrado hoje</Typography>
                    )}
                </CardContent>
            </Card>
            <Card sx ={{ maxWidth: 600}}>
                    <CardContent>
                        <Typography variant="h5" gutterBottom>
                            Vencedores da Semana
                        </Typography>
                        {vencedoresSemana.length > 0 ?(
                            <ul>
                                {vencedoresSemana.map((restaurante,index) => (
                                    <li key={index}>
                                        <Typography>
                                            {restaurante.data} - {restaurante.restauranteNome} ({restaurante.totalVotos} votos)
                                        </Typography>
                                    </li>
                                ))}
                            </ul>
                        ) : (
                            <Typography>Nenhum vencedor na ultima semana.</Typography>
                        )}
                    </CardContent>
            </Card>
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
        </Box>
    )
}
export default Home